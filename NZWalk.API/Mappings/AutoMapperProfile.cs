using AutoMapper;

namespace NZWalk.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, UserDomain>()
                .ForMember(x => x.Name, opt=> opt.MapFrom(x=> x.FullName))  // for non matching properties define mapping 
                .ReverseMap(); // ReverseMap means UserDTO to UserDomain and also UserDomain to UserDTO
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
