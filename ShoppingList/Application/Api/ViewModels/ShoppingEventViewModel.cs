using System;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.ViewModels
{
    public class ShoppingEventViewModel
    {
        public ShoppingEventViewModel(ShoppingEvent shoppingEvent)
        {
            if (shoppingEvent == null)
            {
                throw new ArgumentException("shoppingEvent");
            }

            Id = shoppingEvent.Id;
            VendorId = shoppingEvent.VendorId ?? 0;
            VendorName = shoppingEvent.Vendor != null ? shoppingEvent.Vendor.Name : "";
            Date = shoppingEvent.ShoppingDate;
            DoneShopping = shoppingEvent.DoneShopping;
        }

        public int Id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime Date { get; set; }
        public bool DoneShopping { get; set; }
    }
}