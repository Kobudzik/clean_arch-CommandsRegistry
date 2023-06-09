﻿using System;
using AutoMapper;
using CommandsRegistry.Application.Common.Mappings;
using CommandsRegistry.Domain.Entities.Core;

namespace CommandsRegistry.Application.Users.Commands.UpdateUser.Request
{
    public class UpdateUserModel : IMapFrom<UserAccount>
    {
        public UpdateUserModel()
        {
        }

        public UpdateUserModel(
            Guid userId,
            string firstName,
            string lastName,
            string themePrimaryColor)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            ThemePrimaryColor = themePrimaryColor;
        }

        public Guid UserId { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string ThemePrimaryColor { get; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserModel, UserAccount>();
        }
    }
}