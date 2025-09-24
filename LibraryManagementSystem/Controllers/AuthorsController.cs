using LibraryManagementSystem.Data;
using LibraryManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

public class AuthorsController(LibraryContext context) : Controller
{
    public IActionResult Authors()
    {
        // DO NOT MODIFY ABOVE THIS LINE
        // Refer to similar listing for Members
        var authors = context.Authors.Include(a => a.Books).ToList();
        return View(authors);
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Author author)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        if (ModelState.IsValid)
        {
            // Trim whitespace, check for empty strings
            if (string.IsNullOrWhiteSpace(author.Name))
            {
                ModelState.AddModelError("Name", "Author name cannot be empty or whitespace only.");
                return View(author);
            }
            
            author.Name = author.Name.Trim();
            context.Authors.Add(author);
            context.SaveChanges();
            return RedirectToAction("Authors");
        }
        return View(author);
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        var author = context.Authors.Find(id);
        if (author == null)
        {
            return NotFound();
        }
        context.Authors.Remove(author);
        context.SaveChanges();
        return RedirectToAction("Authors");
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        var author = context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }
        return View(author);
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpPost]
    public IActionResult Update(Author author)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        if (ModelState.IsValid)
        {
            var existing = context.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (existing == null)
            {
                return NotFound();
            }
            
            // Trim whitespace and check for empty strings
            if (string.IsNullOrWhiteSpace(author.Name))
            {
                ModelState.AddModelError("Name", "Author name cannot be empty or whitespace only.");
                return View(author);
            }
            
            existing.Name = author.Name.Trim();
            context.SaveChanges();
            return RedirectToAction("Authors");
        }
        return View(author);
        // DO NOT MODIFY BELOW THIS LINE
    }
}