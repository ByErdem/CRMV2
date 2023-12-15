using AutoMapper;
using CRM.Entities.Concrete;
using CRM.Entities.Dtos;


namespace CRM.Services.Automapper.Profiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserLoginDto, User>();
			CreateMap<User, UserLoginDto>();
		}
	}
}
