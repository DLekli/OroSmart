using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OroSmart.Data.Pagination;
using OroSmart.Data;
using OroSmart.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OroSmart.Data.Validator;
using Microsoft.AspNetCore.Authorization;

namespace OroSmart.Controllers
{
    [Authorize]
    public class ContactTypeController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 5;

        public ContactTypeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, string name, string note, int page = 1)
        {
            var query = _context.ContactTypes.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(ct => ct.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(ct => ct.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(note))
            {
                query = query.Where(ct => ct.Note.Contains(note));
            }

            query = query.OrderByDescending(ct => ct.Id);

            var totalItems = await query.CountAsync();

            var paginatedContactTypes =  PaginatedList<ContactType>.Create(query, page, PageSize);

            ViewBag.Id = id;
            ViewBag.Name = name;
            ViewBag.Note = note;

            return View(paginatedContactTypes);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Note")] ContactType contactType)
        {
            var validator = new ContactTypeValidator(); 
            var validationResult = await validator.ValidateAsync(contactType);

            if (validationResult.IsValid && ModelState.IsValid)
            {
                try
                {
                    

                    _context.ContactTypes.Add(contactType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the contact type. Please try again.");
                    return View(contactType);
                }
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(contactType);
            }
        }

        public async Task<IActionResult> View(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _context.ContactTypes.FindAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var contactType = await _context.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }
            return View(contactType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Note")] ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactTypeExists(contactType.Id))
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
            return View(contactType);
        }

        // Delete Action
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _context.ContactTypes.FindAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactType = await _context.ContactTypes.FindAsync(id);
            _context.ContactTypes.Remove(contactType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ContactTypeExists(int id)
        {
            return _context.ContactTypes.Any(e => e.Id == id);
        }


    }
}
