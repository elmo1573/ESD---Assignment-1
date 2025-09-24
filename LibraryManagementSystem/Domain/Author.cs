using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain;

public class Author : BaseModel
{
    // DO NOT MODIFY ABOVE THIS LINE
    // TODO: 4.1 Add public Name property here with type 'string?' (nullable string)
    [Required(ErrorMessage = "Author name is required.")]
    [StringLength(200, ErrorMessage = "Author name cannot exceed 200 characters.")]
    [MinLength(1, ErrorMessage = "Author name cannot be empty.")]
    public string? Name { get; set; }
    // TODO: 4.2 Add public Books property here with type 'ICollection<Book>' (collection of Book)
    // An author may have written multiple books.
    // This will make the relationship between Book and Author many-to-many
    public ICollection<Book> Books { get; set; } = new List<Book>();
    // DO NOT MODIFY BELOW THIS LINE

    public string BooksToString()
    {
        // DO NOT MODIFY ABOVE THIS LINE
        // This method should return a string with the names of the books of the author separated by commas
        // If the author has multiple books, the names should be separated by commas and the last name should be preceded by 'and'
        // If the author has only one book, the name should be returned as is
        // If the author has no books, an empty string should be returned
        // TODO: 4.3 Implement the BooksToString method
        if (Books == null || Books.Count == 0)
        {
            return string.Empty;
        }

        var titles = Books.Select(b => b.Title).ToList();
        if (titles.Count == 1)
        {
            return titles[0];
        }

        var allButLast = titles.Take(titles.Count - 1);
        var last = titles[^1];
        return $"{string.Join(", ", allButLast)} and {last}";
        // DO NOT MODIFY BELOW THIS LINE
    }
}