﻿using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using CommandsRegistry.Application.Common.Interfaces;

namespace CommandsRegistry.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserManagementService _userManagementService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IUserManagementService userManagementService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _userManagementService = userManagementService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userPublicId = _currentUserService.UserGuid();
            var userName = string.Empty;

            if (userPublicId != Guid.Empty)
            {
                var userDetails = await _userManagementService.GetUserDetailsAsync(userPublicId);
                userName = userDetails.UserName;
            }

            _logger.LogInformation("Request: {Name} {@UserPublicId} {@UserName} {@Request}",
                requestName, userPublicId, userName, request);
        }
    }
}
