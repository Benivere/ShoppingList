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
    
    public partial class Vendor
    {
        public Vendor()
        {
            this.ItemVendorXref = new HashSet<ItemVendorXref>();
            this.ShoppingEvent = new HashSet<ShoppingEvent>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<ItemVendorXref> ItemVendorXref { get; set; }
        public virtual ICollection<ShoppingEvent> ShoppingEvent { get; set; }
    }
}