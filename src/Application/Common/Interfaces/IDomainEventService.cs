using System.Threading.Tasks;
using CommandsRegistry.Domain.Common;

namespace CommandsRegistry.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
