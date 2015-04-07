//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoppingList.Application.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShoppingEvent
    {
        public ShoppingEvent()
        {
            this.ShoppingItem = new HashSet<ShoppingItem>();
        }
    
        public int Id { get; set; }
        public Nullable<int> VendorId { get; set; }
        public System.DateTime ShoppingDate { get; set; }
        public bool DoneShopping { get; set; }
    
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<ShoppingItem> ShoppingItem { get; set; }
    }
}