using AutoMapper;
using LibrarySystem.Domain.Models;
using LibrarySystem.SharedKernel.DTOs;

namespace LibrarySystem.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap().ForMember(x => x.Id, y => y.Ignore());
            
            CreateMap<Author, AuthorDTO>().ReverseMap().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<Loan, LoanDTO>().ReverseMap().ForMember(x => x.Id, y => y.Ignore());
            CreateMap<Loan, CreateLoanDTO>().ReverseMap();
            CreateMap<Loan, UpdateLoanDTO>().ReverseMap();

            CreateMap<Reader, ReaderDTO>().ReverseMap().ForMember(x => x.Id, y => y.Ignore());
        }
    }
}
