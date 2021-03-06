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
    
    public partial class ShopEntities : DbContext
    {
        public ShopEntities()
            : base("name=ShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Store> Stores { get; set; }
    
        public virtual int sp_insertshop(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_insertshop", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<getinventory_Result> getinventory(string store)
        {
            var storeParameter = store != null ?
                new ObjectParameter("store", store) :
                new ObjectParameter("store", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getinventory_Result>("getinventory", storeParameter);
        }
    
        public virtual int sp_addInventory(string item, Nullable<decimal> price, string shopID)
        {
            var itemParameter = item != null ?
                new ObjectParameter("item", item) :
                new ObjectParameter("item", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("price", price) :
                new ObjectParameter("price", typeof(decimal));
    
            var shopIDParameter = shopID != null ?
                new ObjectParameter("shopID", shopID) :
                new ObjectParameter("shopID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_addInventory", itemParameter, priceParameter, shopIDParameter);
        }
    
        public virtual ObjectResult<sp_getinventory_Result> sp_getinventory(string store)
        {
            var storeParameter = store != null ?
                new ObjectParameter("store", store) :
                new ObjectParameter("store", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getinventory_Result>("sp_getinventory", storeParameter);
        }
    }
}
