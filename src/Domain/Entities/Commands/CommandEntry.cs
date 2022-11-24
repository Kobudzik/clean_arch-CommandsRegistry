using CommandsRegistry.Domain.Common;

namespace CommandsRegistry.Domain.Entities.Commands
{
    public class CommandEntry: AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JsonSchema { get; set; }
    }
}
