using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OroSmart.Data;
using OroSmart.Data.ViewModels;
using OroSmart.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using OroSmart.Data.Pagination;
using OroSmart.Data.Validator;

namespace OroSmart.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PageSize = 5;

        public CustomerController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int? id, string name, string vat, DateTime? dateOfRegistration,
            bool? active, string firstEntryUser, string lastUpdateUser, DateTime? lastUpdateTimestamp, int page = 1)
        {
            var query = _context.Customers.AsQueryable();

            query = query.Include(c => c.CustomersAdded)
                         .Include(h => h.CustomersLastUpdated);

            if (id.HasValue)
            {
                query = query.Where(c => c.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(vat))
            {
                query = query.Where(c => c.VAT.Contains(vat));
            }
            if (dateOfRegistration.HasValue)
            {
                query = query.Where(c => c.DateOfRegistration == dateOfRegistration);
            }
            if (active.HasValue)
            {
                query = query.Where(c => c.Active == active);
            }
            if (!string.IsNullOrEmpty(firstEntryUser))
            {
                query = query.Where(c => c.CustomersAdded.UserName.Contains(firstEntryUser));
            }
            if (!string.IsNullOrEmpty(lastUpdateUser))
            {
                query = query.Where(c => c.CustomersLastUpdated.UserName.Contains(lastUpdateUser));
            }
            if (lastUpdateTimestamp.HasValue)
            {
                query = query.Where(c => c.last_update_Timestamp == lastUpdateTimestamp);
            }

            query = query.OrderByDescending(c => c.Id);

            var totalItems = await query.CountAsync();

            var paginatedCustomers = PaginatedList<Customer>.Create(query, page, PageSize);

            ViewBag.Id = id;
            ViewBag.Name = name;
            ViewBag.VAT = vat;
            ViewBag.DateOfRegistration = dateOfRegistration;
            ViewBag.Active = active;
            ViewBag.FirstEntryUser = firstEntryUser;
            ViewBag.LastUpdateUser = lastUpdateUser;
            ViewBag.LastUpdateTimestamp = lastUpdateTimestamp;

            return View(paginatedCustomers);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,VAT,DateOfRegistration,Active")] Customer customer)
        {
            var validator = new CustomerValidator();
            var validationResult = await validator.ValidateAsync(customer);

            if (validationResult.IsValid && !ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                customer.first_entry_user_id = user.Id;

                customer.last_update_user_id = user.Id;
                customer.last_update_Timestamp = DateTime.UtcNow;

                _context.Add(customer);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Edit), new { id = customer.Id });

            }

            else
            {
                // If validation fails, add model errors to ModelState
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                // Return the view with the model to show the validation errors
                var viewModel = new CustomerViewModel
                {
                    Customer = customer
                };

                return View(viewModel);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            var workLocation = await _context.CustomersWorkLocations.FirstOrDefaultAsync(c => c.CustomerId == id);

            var viewModel = new CustomerViewModel
            {
                Customer = customer,
                WorkLocation = workLocation
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,VAT,DateOfRegistration,Active")] Customer customer)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (id != customer.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {

                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var existingCustomer = await _context.Customers.FindAsync(id);

                    customer.first_entry_user_id = existingCustomer.first_entry_user_id;

                    _context.Entry(existingCustomer).CurrentValues.SetValues(customer);

                    existingCustomer.last_update_user_id = userId;
                    existingCustomer.last_update_Timestamp = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }



        //View
        public async Task<IActionResult> View(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var workLocation = await _context.CustomersWorkLocations.FirstOrDefaultAsync(c => c.CustomerId == id);

            var viewModel = new CustomerViewModel
            {
                Customer = customer,
                WorkLocation = workLocation
            };

            return View(viewModel);

        }


        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.CustomersAdded)
                .Include(c => c.CustomersLastUpdated)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
