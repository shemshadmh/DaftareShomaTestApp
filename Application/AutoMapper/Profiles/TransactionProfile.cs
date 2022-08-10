
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Application.Transactions.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<CreateTransactionDto, Transaction>()
                .ForMember(dest => 
                    dest.Date, opt => 
                    opt.MapFrom(src => DateTime.Parse(src.TransactionDate)))
                .ForMember(dest =>
                    dest.Id, opt =>
                    opt.MapFrom(src => src.TransactionId));

            CreateMap<Transaction, ReportTransactionDto>()
                .ForMember(dest =>
                    dest.Date, opt =>
                    opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US", true))))
                .ForMember(dest =>
                    dest.Fullname, opt =>
                    opt.MapFrom(src => $"{src.Person.Name} {src.Person.Family}"));
        }
    }
}
