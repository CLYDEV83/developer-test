using System.Collections.Generic;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class PropertyViewModel
    {
        public PropertyViewModel()
        {

        }

        public PropertyViewModel(Models.Property property)
        {
            Id = property.Id;
            StreetName = property.StreetName;
            Description = property.Description;
            NumberOfBedrooms = property.NumberOfBedrooms;
            PropertyType = property.PropertyType;
            IsListedForSale = property.IsListedForSale;
        }

        public string StreetName { get; set; }
        public string Description { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string PropertyType { get; set; }
        public int Id { get; set; }
        public bool IsListedForSale { get; set; }
        public List<BuyerOfferViewModel> MyOffers { get; set; }
    }
}