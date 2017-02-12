using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class ViewingViewModel
    {
        public ViewingViewModel()
        {

        }

        public ViewingViewModel(Viewing viewing)
        {
            PropertyId = viewing.PropertyId;
            BuyerUserId = viewing.BuyerUserId;
            Date = viewing.Date;
            Time = viewing.Time;
            Status = viewing.Status;
        }

        public int PropertyId { get; set; }

        public string BuyerUserId { get; set; }

        public string StreetName { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        public BookingStatus Status { get; set; }
    }
}