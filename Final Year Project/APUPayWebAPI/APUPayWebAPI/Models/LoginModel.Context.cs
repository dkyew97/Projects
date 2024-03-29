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
    
    public partial class APUPayEntities : DbContext
    {
        public APUPayEntities()
            : base("name=APUPayEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<APUUser> APUUsers { get; set; }
    
        public virtual int sp_updatebalance(string username, Nullable<decimal> balance, string type)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var balanceParameter = balance.HasValue ?
                new ObjectParameter("balance", balance) :
                new ObjectParameter("balance", typeof(decimal));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_updatebalance", usernameParameter, balanceParameter, typeParameter);
        }
    
        public virtual int sp_registration(string type, string username, string password, string email, Nullable<decimal> balance, string name)
        {
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var balanceParameter = balance.HasValue ?
                new ObjectParameter("balance", balance) :
                new ObjectParameter("balance", typeof(decimal));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_registration", typeParameter, usernameParameter, passwordParameter, emailParameter, balanceParameter, nameParameter);
        }
    
        public virtual ObjectResult<sekecall_Result> sekecall()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sekecall_Result>("sekecall");
        }
    
        public virtual int sp_insertsho(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_insertsho", usernameParameter, passwordParameter);
        }
    }
}
