using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommand
    {
        public int PropertyId { get; set; }

        public string BuyerUserId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }
    }
}