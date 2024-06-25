namespace LibrarySystem.SharedKernel.DTOs
{
    public class LoanDTO
    {
        public BookDTO Book { get; set; }
        public ReaderDTO Reader { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class CreateLoanDTO
    {
        public int BookId { get; set; }
        public int ReaderId { get; set; }
       //public DateTime LoanDate { get; set; }
    }

    public class UpdateLoanDTO
    {
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
