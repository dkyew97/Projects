//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APUPayWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionRecord
    {
        public string transaction_id { get; set; }
        public System.DateTime transaction_date { get; set; }
        public string store_id { get; set; }
        public string user_id { get; set; }
        public Nullable<decimal> transaction_amount { get; set; }
    }
}
