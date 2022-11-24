using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Models;
using MediatR;

namespace CommandsRegistry.Application.Users.Queries.GetAllUsers
{
    public class GetPaginatedUsersQuery : IRequest<PaginatedList<UserListItemModel>>
    {
        public GetPaginatedUsersQuery(Pager pager, GetPaginatedUsersFilterModel filter)
        {
            this.Pager = pager;
            this.Filter = filter;
        }

        private Pager Pager { get; }
        private GetPaginatedUsersFilterModel Filter { get; }

        internal class Handler : IRequestHandler<GetPaginatedUsersQuery, PaginatedList<UserListItemModel>>
        {
            private readonly IUserManagementService _userManagementService;

            public Handler(IUserManagementService userManagementService)
            {
                _userManagementService = userManagementService;
            }

            public async Task<PaginatedList<UserListItemModel>> Handle(GetPaginatedUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _userManagementService.GetUsersAsync(request.Pager, request.Filter);

                return new PaginatedList<UserListItemModel>(users, request.Pager.TotalRows, request.Pager.Index, request.Pager.Size);
            }
        }
    }
}