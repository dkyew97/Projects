using APUPayWebAPI.Models;
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
    public class TransactionController : ApiController
    {
        APUPayEntities1 db = new APUPayEntities1();
        [HttpPost]
        [ActionName("LogTransaction")]
        public HttpResponseMessage LogTransaction(string user_id, string store_id, DateTime date, string transactionID, decimal amount)
        {
            try
            {
                TransactionRecord transactionRecord = new TransactionRecord();
                transactionRecord.store_id = store_id;
                transactionRecord.user_id = user_id;
                transactionRecord.transaction_date = date;
                transactionRecord.transaction_id = transactionID;
                transactionRecord.transaction_amount = amount;
                db.TransactionRecords.Add(transactionRecord);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Successful");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }
       
        [HttpGet]
        [ActionName("GetTransactionWithDateStore")]
        public HttpResponseMessage GetTransactionWithDateStore(DateTime dateTime,string storeID)
        {
            try
            {
                var x = db.sp_getHistoryStore(dateTime,storeID);
                return Request.CreateResponse(HttpStatusCode.Accepted, x);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }
        [HttpGet]
        [ActionName("GetAllTransaction")]
        public HttpResponseMessage GetAllTransaction(string storeID)
        {
            try
            {
                var x = db.sp_getAllHistoryStore(storeID);
                return Request.CreateResponse(HttpStatusCode.Accepted, x);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }
        [HttpGet]
        [ActionName("GetTransactionUser")]
        public HttpResponseMessage GetTransactionUser(string userID)
        {
            try
            {
                var x = db.sp_getAllHistoryUser(userID);
                return Request.CreateResponse(HttpStatusCode.Accepted, x);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
        }

    }
}
