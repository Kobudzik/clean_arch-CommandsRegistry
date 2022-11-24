using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces;

namespace CommandsRegistry.Application.Users.Queries.GetUserSettings
{
    public class GetUserSettingsQuery : IRequest<SettingsVM>
    {
        public Guid PublicId { get; set; }

        internal sealed class GetSettingsQueryHandler : IRequestHandler<GetUserSettingsQuery, SettingsVM>
        {
            private readonly IUserManagementService _userManagementService;

            public GetSettingsQueryHandler(IUserManagementService userManagementService)
            {
                _userManagementService = userManagementService;
            }

            public async Task<SettingsVM> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
            {
                var userSettings = await _userManagementService.GetUserSettingsAsync(request.PublicId);

                return userSettings;
            }
        }
    }
}