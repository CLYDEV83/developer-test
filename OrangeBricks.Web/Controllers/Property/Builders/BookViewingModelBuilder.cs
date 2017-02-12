using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class BookViewingModelBuilder
    {

        private readonly IOrangeBricksContext _context;

        public BookViewingModelBuilder(IOrangeBricksContext context)
        {
            _context = context;

        }

        public ViewingViewModel Build(int id)
        {
            var property = _context.Properties.Find(id);

            return new ViewingViewModel
            {
                PropertyId = property.Id,
                StreetName = property.StreetName
            };
        }
    }
}