namespace LibrarySystem.Domain.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
