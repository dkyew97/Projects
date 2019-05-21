using APUPayWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APUPayWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        APUPayEntities db = new APUPayEntities();
        [HttpGet]
        [ActionName("Login")] 
        public HttpResponseMessage Login(string username, string password)
        {
            APUUser user = db.APUUsers.Where(x => x.user_id == username && x.user_password == password).FirstOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid TP Number or Password. Please Check Again");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, user);
            }
        }
        [HttpPost]
        [ActionName("UpdateBalance")]
        public HttpResponseMessage UpdateBalance(string id, decimal balance,string transactionType)
        {
            if (ModelState.IsValid)
            {

                APUUser user = db.APUUsers.Where(x => x.user_id == id).FirstOrDefault();
                if (user != null)
                {
                    db.sp_updatebalance(id, balance, transactionType);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                    return response;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Rejected");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Rejected");
            }
        }
        [HttpPost]
        [ActionName("Registration")]
        public HttpResponseMessage Registration(string type, string username,string password,string email, decimal balance,string name)
        {
            try
            {
                db.sp_registration(type,username,password,email,balance,name);
                return Request.CreateResponse(HttpStatusCode.Accepted, "Successful");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }
        [HttpGet]
        [ActionName("AdminLogin")]
        public HttpResponseMessage AdminLogin(string username, string password, string type)
        {
            APUUser user = db.APUUsers.Where(x => x.user_id == username && x.user_password == password && x.user_type == type).FirstOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter valid UserName and Password");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Success");
            }
        }
    }
}
