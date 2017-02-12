using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;
        public BookViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(BookViewingCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);

            var viewing = new Viewing
            {
                PropertyId = property.Id,
                Status = BookingStatus.Unconfirmed,
                Date = command.Date,
                Time = command.Time,
                BuyerUserId = command.BuyerUserId,
            };

            try
            {
                if (property.Viewings == null)
                {
                    property.Viewings = new List<Viewing>();
                }

                property.Viewings.Add(viewing);
            }
            catch (Exception ee)
            {

                throw new Exception(ee.ToString());
            }

            _context.SaveChanges();
        }
    }
}