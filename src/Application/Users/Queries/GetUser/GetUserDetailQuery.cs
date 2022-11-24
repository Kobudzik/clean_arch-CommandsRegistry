using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Authentication.DTOs;
using CommandsRegistry.Application.Common.Interfaces;

namespace CommandsRegistry.Application.Users.Queries.GetUser
{
    public class GetUserDetailQuery : IRequest<UserDto>
    {
        public Guid PublicId { get; set; }

        internal sealed class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDto>
        {
            private readonly IUserManagementService _userManagementService;
            private readonly IApplicationDbContext _context;

            public GetUserDetailQueryHandler(IUserManagementService userManagementService, IApplicationDbContext context)
            {
                _userManagementService = userManagementService;
                _context = context;
            }

            public async Task<UserDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
            {
                var userDetails = await _userManagementService.GetUserDetailsAsync(request.PublicId);

                return userDetails;
            }
        }
    }
}