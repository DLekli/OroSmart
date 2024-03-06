using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OroSmart.Data;
using OroSmart.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OroSmart.Controllers
{
    public class CustomersWorkLocationController : Controller
    {
        private readonly AppDbContext _context;

        public CustomersWorkLocationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWorkLocation(int? id, int customerId, [Bind("City,Address,PostalCode,IsHeadquarters,Notes,")] CustomersWorkLocation workLocation)
        {

            if (id == null)
            {
                TempData["Error"] = "You need first to create Customer Info";
                return RedirectToAction("Create","Customer");
            }

            customerId = await _context.Customers.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefaultAsync();


            workLocation.CustomerId = customerId;

            _context.Add(workLocation);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Customer");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWorkLocation(int customerId, [Bind("Id,CustomerId,City,Address,PostalCode,IsHeadquarters,Notes,ReferencePersonId")] CustomersWorkLocation workLocation)
        {
            if (customerId <= 0)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    var existingWorkLocation = await _context.CustomersWorkLocations.FirstOrDefaultAsync(w => w.CustomerId == customerId);
                    if (existingWorkLocation == null)
                    {
                        workLocation.CustomerId = customerId;
                        _context.CustomersWorkLocations.Add(workLocation);
                    }
                    else
                    {
                        // Update work location data
                        existingWorkLocation.City = workLocation.City;
                        existingWorkLocation.Address = workLocation.Address;
                        existingWorkLocation.PostalCode = workLocation.PostalCode;
                        existingWorkLocation.IsHeadquarters = workLocation.IsHeadquarters;
                        existingWorkLocation.Notes = workLocation.Notes;
                        existingWorkLocation.ReferencePersonId = workLocation.ReferencePersonId;

                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }

                return RedirectToAction("Index", "Customer");
            }

            return View(workLocation);
        }




    }
}
