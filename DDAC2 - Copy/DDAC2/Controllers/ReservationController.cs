using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDAC2.Data;
using DDAC2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DDAC2.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

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


        public async Task<IActionResult> MakeReservation(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var carModel = await _context.CarModel.FirstOrDefaultAsync(m => m.CarVinNumber == id);
            if (carModel == null)
            {
                return NotFound();
            }
            Reservation reservation = new Reservation();
            reservation.CarVinNumber = carModel.CarVinNumber;
            reservation.Status = "Pending";
            return View(reservation);
        }

        [HttpPost]
        [ActionName("MakeReservation")]
        public async Task<IActionResult> MakeReservation([Bind("ReservationID,BuyerName,BuyerEmail,BuyerPhone,CarVinNumber,Status")] Reservation reservation)
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
            return RedirectToAction("Index","Home");
        }
    }
}