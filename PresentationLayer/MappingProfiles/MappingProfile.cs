using AutoMapper;
using DatabaseLayer.Model;
using PresentationLayer.ViewModels;

namespace PresentationLayer.MappingProfiles;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        CreateMap<MyProductVM, MyProduct>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => ConvertPrice(src.Price)));
        CreateMap<MyProduct, MyProductVM>();
        CreateMap<List<MyProduct>, HomePageVM>();
    }

    private decimal ConvertPrice(string price)
    {
        if (decimal.TryParse(price, out decimal result))
            return result;

        throw new ArgumentException("Invalid price format", nameof(price));
    }

}
