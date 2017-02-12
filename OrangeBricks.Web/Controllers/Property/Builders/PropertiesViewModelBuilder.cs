using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class PropertiesViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public PropertiesViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public PropertiesViewModel Build(PropertiesQuery query, string userId)
        {

            var properties = _context.Properties
                .Where(p => p.IsListedForSale)
                .Include(x => x.Offers)
                .Include(v => v.Viewings);
                
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                properties = properties.Where(x => x.StreetName.Contains(query.Search)
                    || x.Description.Contains(query.Search));
            }

            var propListings = new PropertiesViewModel
            {
                Properties = MapViewModels(properties, userId),
                Search = query.Search
            };


            return propListings;
        }

        private List<PropertyViewModel> MapViewModels(IQueryable<Models.Property> properties, string userId)
        {
            var propertyModels = new List<PropertyViewModel>();

            properties.ToList()
                .ForEach(p => propertyModels.Add(CreatePropertyModel(p, userId)));


            return propertyModels;
        }

        private PropertyViewModel CreatePropertyModel(Models.Property property, string userId)
        {

            var propertyViewModel = new PropertyViewModel(property);

            if (property.Offers.Any(o => o.BuyerUserId == userId))
            {
                propertyViewModel.MyOffers = GetUsersOffersForProperty(property, userId);
            }

            if (property.Viewings.Any(o => o.BuyerUserId == userId))
            {
                propertyViewModel.MyViewings = GetUsersViewingsForProperty(property, userId);
            }

            return propertyViewModel;
        }

        private List<BuyerOfferViewModel> GetUsersOffersForProperty(Models.Property property, string userId)
        {
            var myOffers = new List<BuyerOfferViewModel>();

            var offers = property.Offers
                .Where(p => p.BuyerUserId == userId && p.propertyId == property.Id)
                .OrderByDescending(q => q.CreatedAt);

            if (offers != null)
            {
                myOffers = offers.Select(o => new BuyerOfferViewModel(o))
                .ToList();
            }

            return myOffers;
        }

        private List<ViewingViewModel> GetUsersViewingsForProperty(Models.Property property, string userId)
        {
            var myViewings = new List<ViewingViewModel>();

            var viewings = property.Viewings
                .Where(o => o.BuyerUserId == userId && o.PropertyId == property.Id);

            if (viewings != null)
            {
                myViewings = viewings.Select(o => new ViewingViewModel(o))
                .ToList();
            }

            return myViewings;
        }
    }
}