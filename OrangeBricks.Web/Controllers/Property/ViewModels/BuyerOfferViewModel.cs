using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class BuyerOfferViewModel
    {

        public BuyerOfferViewModel()
        {

        }
        public BuyerOfferViewModel(Offer offer)
        {
            BuyerUserId = offer.BuyerUserId;
            Amount = offer.Amount;
            Status = offer.Status;
            OfferDate = offer.CreatedAt;
        }

        public int? Amount { get; set; }

        public OfferStatus Status { get; set; }

        public DateTime? OfferDate { get; set; }

        public string BuyerUserId { get; set; }
    }
}