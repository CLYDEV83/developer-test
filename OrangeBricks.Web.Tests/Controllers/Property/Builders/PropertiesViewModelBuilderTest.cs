using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Models;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{
    public static class ExtentionMethods
    {
        public static IDbSet<T> Initialize<T>(this IDbSet<T> dbSet, IQueryable<T> data) where T : class
        {
            dbSet.Provider.Returns(data.Provider);
            dbSet.Expression.Returns(data.Expression);
            dbSet.ElementType.Returns(data.ElementType);
            dbSet.GetEnumerator().Returns(data.GetEnumerator());
            return dbSet;
        }
    }

    [TestFixture]
    public class PropertiesViewModelBuilderTest
    {
        private IOrangeBricksContext _context;
        string myUserId = "me@me.com";
        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnPropertiesWithMatchingStreetNamesWhenSearchTermIsProvided()
        {
            // Arrange
            var builder = new PropertiesViewModelBuilder(_context);

            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "Smith Street", Description = "", IsListedForSale = true, Offers = new List<Offer>() },
                new Models.Property{ StreetName = "Jones Street", Description = "", IsListedForSale = true, Offers = new List<Offer>()}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = "Smith Street"
            };

            // Act
            var viewModel = builder.Build(query, myUserId);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
        }

        [Test]
        public void BuildShouldReturnPropertiesWithMatchingDescriptionsWhenSearchTermIsProvided()
        {
            // Arrange
            var builder = new PropertiesViewModelBuilder(_context);

            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "", Description = "Great location", IsListedForSale = true , Offers = new List<Offer>()},
                new Models.Property{ StreetName = "", Description = "Town house", IsListedForSale = true, Offers = new List<Offer>() }
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = "Town"
            };

            // Act
            var viewModel = builder.Build(query, myUserId);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
            Assert.That(viewModel.Properties.All(p => p.Description.Contains("Town")));
        }

        [Test]
        public void ShouldReturnMyOffers()
        {
            var builder = new PropertiesViewModelBuilder(_context);


            var properties = new List<Models.Property>
            {
                 new Models.Property
                 {
                     StreetName = "Smith Street",
                     Description = "2 bed house",
                     IsListedForSale = true,
                     Offers = new List<Offer>
                 {
                    new Offer
                        {
                             BuyerUserId = "me@me.com",
                             Status = OfferStatus.Pending
                        },
                     new Offer
                         {
                             BuyerUserId = "someone@someone.com",
                             Status = OfferStatus.Rejected
                        }
                 }

                 }


             };


            var testModels = Substitute.For<IDbSet<Models.Property>>().Initialize(properties.AsQueryable());

            _context.Properties.Returns(testModels);

            var result = builder.Build(new PropertiesQuery { Search = "" }, myUserId);

            // Arrange
            var expectedCount = 1;

            // Act
            var list = result.Properties.Select(o => o.MyOffers);
            var actualCount = list.Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        
        }

        [Test]
        public void ShouldReturnMyViewings()
        {

            var builder = new PropertiesViewModelBuilder(_context);

            DateTime today = DateTime.Today;
           

            var properties = new List<Models.Property>
            {

                
                 new Models.Property
                 {
                     StreetName = "Smith Street",
                     Description = "2 bed house",
                     IsListedForSale = true,
                     Offers = new List<Offer>(),
                     Viewings = new List<Viewing>
                     {
                        new Viewing
                        {
                            BuyerUserId = myUserId,
                            Status = Web.Controllers.Property.ViewModels.BookingStatus.Booked,
                            Date = today.Date,
                            Time = today.TimeOfDay,
                            
                        },
                        new Viewing
                        {
                            BuyerUserId = "SomeoneElse@Somewhere.com",
                            Status = Web.Controllers.Property.ViewModels.BookingStatus.Unconfirmed,
                            Date = today.Date,
                            Time = today.TimeOfDay,

                        }
                     }
                 }


             };


            var testModels = Substitute.For<IDbSet<Models.Property>>().Initialize(properties.AsQueryable());

            _context.Properties.Returns(testModels);

            var result = builder.Build(new PropertiesQuery { Search = "" }, myUserId);

            // Arrange
            var expectedCount = 1;

            // Act
            var list = result.Properties.Select(o => o.MyViewings);

            var actualCount = list.Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }
}

