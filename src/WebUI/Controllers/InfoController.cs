﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;

namespace CommandsRegistry.WebUI.Controllers
{
    public class InfoController : ApiControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public InfoController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet("check-health")]
        public async Task<ActionResult<HealthReport>> CheckHealth() 
            => await _healthCheckService.CheckHealthAsync();

        [HttpGet("version")]
        public async Task<ActionResult<string>> GetVersion() 
            => "1.0.0";
    }
}