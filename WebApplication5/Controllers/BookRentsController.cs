using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class BookRentsController : Controller
    {
        private readonly LibraryContext _context;

        public BookRentsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BookRents
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.BookRents.Include(b => b.Book).Include(b => b.Student);
            return View(await libraryContext.ToListAsync());
        }

        // GET: BookRents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRent = await _context.BookRents
                .Include(b => b.Book)
                .Include(b => b.Student)
                .FirstOrDefaultAsync(m => m.BookRentId == id);
            if (bookRent == null)
            {
                return NotFound();
            }

            return View(bookRent);
        }

        // GET: BookRents/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName");
            return View();
        }

        // POST: BookRents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookRent bookRent)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Find(bookRent.BookId);
                book.Qty -= bookRent.Qty;
                _context.Entry(book).State = EntityState.Modified;
                _context.Add(bookRent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookRent.BookId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", bookRent.StudentId);
            return View(bookRent);
        }

        // GET: BookRents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRent = await _context.BookRents.FindAsync(id);
            if (bookRent == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookRent.BookId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", bookRent.StudentId);
            return View(bookRent);
        }

        // POST: BookRents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookRentId,RentDate,Qty,StudentId,BookId")] BookRent bookRent)
        {
            if (id != bookRent.BookRentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookRent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookRentExists(bookRent.BookRentId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookRent.BookId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", bookRent.StudentId);
            return View(bookRent);
        }

        // GET: BookRents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookRent = await _context.BookRents
                .Include(b => b.Book)
                .Include(b => b.Student)
                .FirstOrDefaultAsync(m => m.BookRentId == id);
            if (bookRent == null)
            {
                return NotFound();
            }

            return View(bookRent);
        }

        // POST: BookRents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookRent = await _context.BookRents.FindAsync(id);
            _context.BookRents.Remove(bookRent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookRentExists(int id)
        {
            return _context.BookRents.Any(e => e.BookRentId == id);
        }
    }
}
