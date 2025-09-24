using System;

namespace LibraryManagementSystem.Domain.Services
{
    // The LoanService class is used to encapsulate the business logic related to loans, 
    // such as checking book availability and member borrowing limits, instead of putting 
    // this functionality in the Loan class. This helps to adhere to the Single Responsibility 
    // Principle (SRP) by keeping the Loan class focused on its primary role of representing 
    // a loan entity, while the LoanService class handles the operations and rules associated 
    // with loans.
    public class LoanService
    {
        public Loan LoanBook(Book book, Member member, DateTime loanDate)
        {
            var loan = new Loan
            {
                Book = book,
                Member = member,
                LoanDate = loanDate,
                ReturnDate = null
            };
            // DO NOT MODIFY ABOVE THIS LINE

            if (!book.IsAvailable())
            {
                throw new InvalidOperationException("The book is not available for loan.");
            }
            if (!member.CanBorrow())
            {
                throw new InvalidOperationException("The member has reached their borrowing limit.");
            }
            member.RecordLoan(loan);
            // DO NOT MODIFY BELOW THIS LINE

            return loan;
        }

        public void ReturnBook(Loan loan)
        {
            if (loan.ReturnDate != null)
            {
                throw new InvalidOperationException("The book has already been returned.");
            }

            loan.ReturnDate = DateTime.Now;
            loan.Member.ReturnLoan(loan);
        }
    }
}
