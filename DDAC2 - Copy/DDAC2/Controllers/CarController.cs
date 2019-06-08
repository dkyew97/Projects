using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDAC2.Data;
using DDAC2.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using static System.Net.Mime.MediaTypeNames;

namespace DDAC2.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlobConnection con = new BlobConnection();
        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarModel.ToListAsync());
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .FirstOrDefaultAsync(m => m.CarVinNumber == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> Test, [Bind("CarVinNumber,CarName,CarMake,Price,PictureUrl")] CarModel carModel )
        {
            CloudBlobContainer container = con.blobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference(carModel.CarVinNumber + ".jpg");
            blob.Properties.ContentType = "image/jpg";
            long size = Test.Sum(f => f.Length);// get file size
            var filePath = Path.GetTempFileName();//get temporary file path
            foreach (var formfiles in Test)
            {
                    using (var fileStream = formfiles.OpenReadStream())
                    {
                        await blob.UploadFromStreamAsync(fileStream);
                        carModel.PictureUrl = blob.Uri.OriginalString;
                    }
                
            }

            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            return View(carModel);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CarVinNumber,CarName,CarMake,Price,PictureUrl")] CarModel carModel)
        {
            if (id != carModel.CarVinNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.CarVinNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .FirstOrDefaultAsync(m => m.CarVinNumber == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var carModel = await _context.CarModel.FindAsync(id);
            _context.CarModel.Remove(carModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(string id)
        {
            return _context.CarModel.Any(e => e.CarVinNumber == id);
        }
    }
}
