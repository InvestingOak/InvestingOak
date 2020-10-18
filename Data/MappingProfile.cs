using AutoMapper;
using InvestingOak.Models;

namespace InvestingOak.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StockOverviewInput, StockOverview>();
            CreateMap<StockQuoteInput, StockQuote>();
            CreateMap<GlobalQuoteInput, GlobalQuote>();
        }
    }
}
