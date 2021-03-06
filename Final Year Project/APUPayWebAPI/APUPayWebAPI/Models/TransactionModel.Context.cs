﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class APUPayEntities1 : DbContext
    {
        public APUPayEntities1()
            : base("name=APUPayEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TransactionRecord> TransactionRecords { get; set; }
    
        public virtual ObjectResult<getrecord_Result> getrecord()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getrecord_Result>("getrecord");
        }
    
        public virtual ObjectResult<getrecordwithdate_Result> getrecordwithdate(Nullable<System.DateTime> date)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getrecordwithdate_Result>("getrecordwithdate", dateParameter);
        }
    
        public virtual ObjectResult<sp_getHistoryStore_Result> sp_getHistoryStore(Nullable<System.DateTime> date, string storeID)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            var storeIDParameter = storeID != null ?
                new ObjectParameter("storeID", storeID) :
                new ObjectParameter("storeID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getHistoryStore_Result>("sp_getHistoryStore", dateParameter, storeIDParameter);
        }
    
        public virtual ObjectResult<sp_getAllHistoryStore_Result> sp_getAllHistoryStore(string storeID)
        {
            var storeIDParameter = storeID != null ?
                new ObjectParameter("storeID", storeID) :
                new ObjectParameter("storeID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getAllHistoryStore_Result>("sp_getAllHistoryStore", storeIDParameter);
        }
    
        public virtual ObjectResult<sp_getAllHistoryUser_Result> sp_getAllHistoryUser(string userid)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getAllHistoryUser_Result>("sp_getAllHistoryUser", useridParameter);
        }
    }
}
