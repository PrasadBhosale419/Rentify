using AutoMapper;
using Backend.Dtos;
using Backend.Models;
using WebAPI.Dtos;
using WebAPI.Models;

namespace Backend.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDTO>().ReverseMap();

            CreateMap<City, CityUpdateDTO>().ReverseMap();

            CreateMap<Property, PropertyDto>().ReverseMap();

            CreateMap<Property, PropertyListDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City.name))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.City.Country))
                .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
                .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name))
                .ForMember(d => d.Photo, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsPrimary).ImageUrl));

            CreateMap<Property, PropertyDetailDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City.name))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.City.Country))
                .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
                .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));

            CreateMap<PropertyType, KeyValuePairDto>();

            CreateMap<FurnishingType, KeyValuePairDto>();

            CreateMap<Photo, PhotoDto>().ReverseMap();
        }
    }
}
