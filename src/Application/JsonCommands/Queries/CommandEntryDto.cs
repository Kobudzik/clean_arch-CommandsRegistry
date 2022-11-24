using AutoMapper;
using CommandsRegistry.Application.Common.Mappings;
using CommandsRegistry.Domain.Entities.Commands;

namespace CommandsRegistry.Application.JsonCommands.Queries
{
    public class CommandEntryDto : IMapFrom<CommandEntry>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string JsonSchema { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CommandEntry, CommandEntryDto>()
                .ReverseMap();
        }
    }
}
