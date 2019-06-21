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
    public class ReturnBooksController : Controller
    {
        private readonly LibraryContext _context;

        public ReturnBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ReturnBooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ReturnBook.Include(r => r.BookRent);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ReturnBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnBook = await _context.ReturnBook
                .Include(r => r.BookRent)
                .FirstOrDefaultAsync(m => m.ReturnBookId == id);
            if (returnBook == null)
            {
                return NotFound();
            }

            return View(returnBook);
        }

        // GET: ReturnBooks/Create
        public IActionResult Create()
        {
            var booklist = _context.BookRents.Where(b => b.Qty > 0).ToList();
            var studentlist = (from brent in _context.BookRents
                               join s in _context.Students on brent.StudentId equals s.StudentId
                               select new Student
                               {
                                   StudentId = brent.StudentId,
                                   StudentName = s.StudentName
                               }).ToList();
            ViewData["Studentlist"] = new SelectList(studentlist, "StudentId", "StudentName");
            ViewData["BookRentId"] = new SelectList(_context.BookRents, "BookRentId", "BookRentId");
            return View();
        }

        // POST: ReturnBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ReturnBook returnBook)
        {
            if (ModelState.IsValid)
            {
                var book = _context.BookRents.Find(returnBook.BookRentId);
                var books = _context.Books.Find(book.BookId);
                books.Qty += returnBook.ReturnQty;
                var bookrent = _context.BookRents.Find(returnBook.BookRentId);
                bookrent.Qty = returnBook.ReturnQty;
                _context.Entry(books).State = EntityState.Modified;

                _context.Add(returnBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookRentId"] = new SelectList(_context.BookRents, "BookRentId", "BookRentId", returnBook.BookRentId);
            return View(returnBook);
        }

        // GET: ReturnBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnBook = await _context.ReturnBook.FindAsync(id);
            if (returnBook == null)
            {
                return NotFound();
            }
            ViewData["BookRentId"] = new SelectList(_context.BookRents, "BookRentId", "BookRentId", returnBook.BookRentId);
            return View(returnBook);
        }

        // POST: ReturnBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReturnBookId,ReturnDate,ReturnQty,BookRentId")] ReturnBook returnBook)
        {
            if (id != returnBook.ReturnBookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(returnBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnBookExists(returnBook.ReturnBookId))
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
            ViewData["BookRentId"] = new SelectList(_context.BookRents, "BookRentId", "BookRentId", returnBook.BookRentId);
            return View(returnBook);
        }

        // GET: ReturnBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnBook = await _context.ReturnBook
                .Include(r => r.BookRent)
                .FirstOrDefaultAsync(m => m.ReturnBookId == id);
            if (returnBook == null)
            {
                return NotFound();
            }

            return View(returnBook);
        }

        // POST: ReturnBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var returnBook = await _context.ReturnBook.FindAsync(id);
            _context.ReturnBook.Remove(returnBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnBookExists(int id)
        {
            return _context.ReturnBook.Any(e => e.ReturnBookId == id);
        }
    }
}
