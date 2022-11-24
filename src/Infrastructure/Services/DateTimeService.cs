using System;
using CommandsRegistry.Application.Common.Interfaces;

namespace CommandsRegistry.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
