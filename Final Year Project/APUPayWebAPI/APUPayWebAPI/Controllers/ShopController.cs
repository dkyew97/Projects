using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APUPayWebAPI.Models;

namespace APUPayWebAPI.Controllers
{
    public class ShopController : ApiController
    {
        ShopEntities db = new ShopEntities();
        [HttpGet]
        [ActionName("ShopLogin")]
        public HttpResponseMessage ShopLogin(string username, string password)
        {
            Store user = db.Stores.Where(x => x.store_id == username && x.store_password == password).FirstOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter valid UserName and Password");
            }
            else
            {
                var result = db.sp_getinventory(username);
                return Request.CreateResponse(HttpStatusCode.Accepted, result);
            }
        }
        [HttpPost]
        [ActionName("ShopRegistration")]
        public HttpResponseMessage ShopRegistration(string username, string password)
        {
           
            try
            {
                db.sp_insertshop(username, password);
                return Request.CreateResponse(HttpStatusCode.Accepted, "Successful");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }
        [HttpPost]
        [ActionName("AddInventory")]
        public HttpResponseMessage AddInventory(string storeID, string item, decimal price)
        {

            try
            {
                db.sp_addInventory(item, price, storeID);
                return Request.CreateResponse(HttpStatusCode.Accepted, "Successful");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }
    }
}
