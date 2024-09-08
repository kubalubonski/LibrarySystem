

namespace LibrarySystem.SharedKernel.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
		//public int AuthorId { get; set; }
		public AuthorDTO? Author { get; set; }
        public int? Year { get; set; }

        public BookDTO()
        {
            Title = string.Empty;
            Year = null;
            Author = new AuthorDTO { Name = string.Empty };
        }
    };

}
