using LibrarySystem.SharedKernel.DTOs;
using Newtonsoft.Json;

namespace LibrarySystem.BlazorClient.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAll();
    }
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookDTO>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("/book");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var books = JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(content);
                    return books;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error
                Console.WriteLine($"Error fetching books: {ex.Message}");
            }
            return new List<BookDTO>();
        }
    }
}
