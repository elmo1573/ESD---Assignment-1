using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LibraryManagementSystem.Domain
{
    // Notice that BaseModel does not have a corresponding table in the database.
    // Similarly, Book and Author have many-to-many relationship so that requires a third table in the 
    // database called BookAuthor or AuthorBook, but we do not have a class for that.
    // The class hierarchies do not map one-to-one to the database.
    // The ORM (Entity Framework Core), despite these differences, maps the relationships between the classes and database.
    public class Book : BaseModel
    {
        [Required(ErrorMessage = "Book title is required.")]
        [StringLength(200, ErrorMessage = "Book title cannot exceed 200 characters.")]
        [MinLength(1, ErrorMessage = "Book title cannot be empty.")]
        public string Title { get; set; } = default!;
        
        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(20, ErrorMessage = "ISBN cannot exceed 20 characters.")]
        [MinLength(1, ErrorMessage = "ISBN cannot be empty.")]
        public string ISBN { get; set; } = default!;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        // A book may have been written by many authors
        // This will make the relationship between Book and Author many-to-many
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public virtual bool IsAvailable()
        {
            // DO NOT MODIFY ABOVE THIS LINE
            // This method should return true if the book is not currently on loan (No entry in Loans collection)
            // or if it was on loan but has been returned (loan.ReturnDate is not null for all Loans)
            return Loans.All(loan => loan.ReturnDate != null);
            // DO NOT MODIFY BELOW THIS LINE
        }

        
        // This is an example of leaking presentation logic in Domain model. It has nothing to do with domain.
        // Ideally, this method should be part of presentation layer, perhaps in BookViewModel class.
        public string AuthorsToString()
        {
            // DO NOT MODIFY ABOVE THIS LINE
            // This method should return a string with the names of the authors of the book separated by commas
            // If the book has multiple authors, the names should be separated by commas and the last name should be preceded by 'and'
            // If the book has only one author, the name should be returned as is or "unknown" if the author's name is null
            // If the book has no authors, an empty string should be returned
            if (Authors == null || Authors.Count == 0)
            {
                return string.Empty;
            }

            var authorNames = Authors
                .Select(a => string.IsNullOrWhiteSpace(a.Name) ? "unknown" : a.Name)
                .ToList();

            if (authorNames.Count == 1)
            {
                return authorNames[0];
            }

            var allButLast = authorNames.Take(authorNames.Count - 1);
            var last = authorNames[^1];
            return $"{string.Join(", ", allButLast)} and {last}";
            // DO NOT MODIFY BELOW THIS LINE
        }
    }
}
