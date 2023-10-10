using AutoMapper;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Mappings
{

    // Install Nuget Packages 
    //1. AutoMapper
    //2. AutoMapper.Extensions.Microsoft.DependencyInjection  ( Adding dependency in application program.cs )
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, UserDomain>()
                .ForMember(x => x.Name, opt=> opt.MapFrom(x=> x.FullName))  // for non matching properties define mapping 
                .ReverseMap(); // ReverseMap means UserDTO to UserDomain and also UserDomain to UserDTO

            // Automapper for Region and RegionDTO
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();   //For Create method 
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap(); // For Update method 

            //Walks Mapper
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();

        }

        public class UserDTO
        {
            // When properties not name explicitly map using ForMember method  
            public string FullName { get; set; }

            // When properties name is same it is automatically map 
            public string Mobile { get; set; }
        }

        public class UserDomain
        {
            public string Name { get; set; }
            public string Mobile { get; set; }
        }

    }
}
