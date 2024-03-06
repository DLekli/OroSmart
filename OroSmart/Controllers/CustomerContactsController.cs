using Microsoft.AspNetCore.Mvc;
using OroSmart.Data;
using OroSmart.Models;
using Microsoft.EntityFrameworkCore;
using OroSmart.Data;
using OroSmart.Models;
using OroSmart.Data.ViewModels;
using OroSmart.Data.Validator;
using FluentValidation;
using System.Diagnostics;

namespace OroSmart.Controllers
{
    public class CustomerContactsController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerContactsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomerContacts(int? id, int customerId, [Bind("ContactTypeId,FirstName,LastName,Contact,Note")] CustomersContacts customerContacts)
        {

            if (id == null)
            {
                TempData["Error"] = "You need first to create Customer Info";
                return RedirectToAction("Create", "Customer");
            }


            customerId = await _context.Customers.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefaultAsync();


            customerContacts.CustomerId = customerId;

            _context.Add(customerContacts);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Customer");


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomerContact(int customerId, CustomerViewModel customerContact)
        {
            if (customerId <= 0 || customerContact == null || customerContact.CustomersContacts == null)
            {
                return NotFound();
            }
            
            var validator = new ContactValidator();
            var validationResult = await validator.ValidateAsync(customerContact.CustomersContacts);
            if (!validationResult.IsValid)
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return NotFound();
                }

                var workLocation = await _context.CustomersWorkLocations.FirstOrDefaultAsync(c => c.CustomerId == customerId);
                var customerContacts = await _context.CustomersContacts.FirstOrDefaultAsync(c => c.CustomerId == customerId);
                
                var viewModel = new CustomerViewModel
                {
                    Customer = customer,
                    WorkLocation = workLocation,
                    CustomersContacts = customerContacts,
                };

                var contactTypesOther = _context.ContactTypes.ToList();

                ViewBag.ContactTypes = contactTypesOther;
                ViewBag.ActiveTab = "#customercontact";
                return View("~/Views/Customer/Edit.cshtml", viewModel);
            }
            try
            {
                var existingCustomerContact = await _context.CustomersContacts
                    .FirstOrDefaultAsync(w => w.CustomerId == customerId);

                if (existingCustomerContact == null)
                {
                    customerContact.CustomersContacts.CustomerId = customerId;
                    _context.CustomersContacts.Add(customerContact.CustomersContacts);
                }
                else
                {
                    existingCustomerContact.ContactTypeId = customerContact.CustomersContacts.ContactTypeId;
                    existingCustomerContact.FirstName = customerContact.CustomersContacts.FirstName;
                    existingCustomerContact.LastName = customerContact.CustomersContacts.LastName;
                    existingCustomerContact.Contact = customerContact.CustomersContacts.Contact;
                    existingCustomerContact.Note = customerContact.CustomersContacts.Note;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Concurrency error occurred while saving the changes.");
                return View(customerContact); 
            }

            var contactTypes = _context.ContactTypes.ToList();

            ViewBag.ContactTypes = contactTypes;

            return RedirectToAction("Index", "Customer"); 
        }
    }
}
