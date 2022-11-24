using AutoMapper;
using System;
using CommandsRegistry.Application.Common.Mappings;
using CommandsRegistry.Domain.Entities.Core;

namespace CommandsRegistry.Application.Authentication.DTOs
{
    public class UserDto : IMapFrom<UserAccount>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        public string[] Roles { get; set; }
        public string ThemePrimaryColor { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserAccount, UserDto>()
                .ForMember(d => d.Roles, opt => opt.MapFrom<UserDtoRolesResolver>());
        }
    }
}