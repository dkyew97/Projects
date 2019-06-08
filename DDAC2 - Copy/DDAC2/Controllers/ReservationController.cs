using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDAC2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DDAC2.Controllers
{
    public class ReservationController : Controller
    {
        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            var items = await CosmoHelper<Reservation>.GetItemAsync(d => d.Status.Equals("Pending"));
            return View(items);
        }
        [ActionName("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("ReservationID,BuyerName,BuyerEmail,BuyerPhone,CarVinNumber,Status")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                Random r = new Random();
                int rannum = r.Next(0, 99999);
                string receiptnumber = DateTime.Now.ToString("yyyyMMddHHmmss") + rannum.ToString().PadLeft(5, '0');
                reservation.ReservationID = receiptnumber;
                await CosmoHelper<Reservation>.CreateItemAsync(reservation);
                return RedirectToAction("Index");
            }
            return View();
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Reservation item = await
           CosmoHelper<Reservation>.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync([Bind("ReservationID,BuyerName,BuyerEmail,BuyerPhone,CarVinNumber,Status")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await CosmoHelper<Reservation>.UpdateItemAsync(reservation.ReservationID,reservation);
                return RedirectToAction("Index");
            }
            return View(reservation);
        }
    }
}