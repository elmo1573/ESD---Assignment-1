using LibraryManagementSystem.Data;
using LibraryManagementSystem.Domain;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

public class BooksController(LibraryContext context) : Controller
{
    public IActionResult Books()
    {
        var books = context.Books
            .Include(b => b.Loans.Where(l => l.ReturnDate == null))
            // DO NOT MODIFY ABOVE THIS LINE
            // TODO: 10. Include Authors in the query
            // Notice: We will have to use SQL Joins if we were not using ORM like Entity Framework
            .Include(b => b.Authors)
            // DO NOT MODIFY BELOW THIS LINE
            .ToList();
        return View(books);
    }

    public IActionResult Add()
    {
        var model = new BookViewModel
        {
            AvailableAuthors = context.Authors.ToList()
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Add(BookViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Trim whitespace and validate
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                ModelState.AddModelError("Title", "Book title cannot be empty or whitespace only.");
                model.AvailableAuthors = context.Authors.ToList();
                return View(model);
            }
            
            if (string.IsNullOrWhiteSpace(model.ISBN))
            {
                ModelState.AddModelError("ISBN", "ISBN cannot be empty or whitespace only.");
                model.AvailableAuthors = context.Authors.ToList();
                return View(model);
            }
            
            var book = new Book
            {
                Title = model.Title.Trim(),
                ISBN = model.ISBN.Trim(),
                Authors = context.Authors.Where(a => model.SelectedAuthorIds != null && model.SelectedAuthorIds.Contains(a.Id)).ToList()
            };
            context.Books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Books");
        }
        model.AvailableAuthors = context.Authors.ToList();
        return View(model);
    }

    public IActionResult Update(int id)
    {
        var book = context.Books.Include(b => b.Authors).FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }
        var model = new BookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            SelectedAuthorIds = book.Authors.Select(a => a.Id).ToList(),
            AvailableAuthors = context.Authors.ToList()
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Update(BookViewModel model)
    {
        if (ModelState.IsValid)
        {
            var book = context.Books.Include(b => b.Authors).FirstOrDefault(b => b.Id == model.Id);
            if (book == null)
            {
                return NotFound();
            }
            
            // Trim whitespace and validate
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                ModelState.AddModelError("Title", "Book title cannot be empty or whitespace only.");
                model.AvailableAuthors = context.Authors.ToList();
                return View(model);
            }
            
            if (string.IsNullOrWhiteSpace(model.ISBN))
            {
                ModelState.AddModelError("ISBN", "ISBN cannot be empty or whitespace only.");
                model.AvailableAuthors = context.Authors.ToList();
                return View(model);
            }
            
            book.Title = model.Title.Trim();
            book.ISBN = model.ISBN.Trim();
            book.Authors = context.Authors.Where(a => model.SelectedAuthorIds != null && model.SelectedAuthorIds.Contains(a.Id)).ToList();
            context.SaveChanges();
            return RedirectToAction("Books");
        }
        model.AvailableAuthors = context.Authors.ToList();
        return View(model);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var book = context.Books.Find(id);
        if (book != null)
        {
            context.Books.Remove(book);
            context.SaveChanges();
            return RedirectToAction("Books");
        }
        return NotFound();
    }

}