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
    
    public partial class ItemVendorXref
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int VendorId { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
