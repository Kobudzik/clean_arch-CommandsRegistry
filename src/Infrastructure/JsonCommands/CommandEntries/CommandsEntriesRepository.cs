using System;
using CommandsRegistry.Application.Common.Interfaces.JsonCommands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Domain.Entities.Commands;
using AutoMapper;
using CommandsRegistry.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using CommandsRegistry.Application.Common.Models;
using CommandsRegistry.Application.JsonCommands.Queries.GetPaginatedJsonCommands;
using LinqKit;
using AutoMapper.QueryableExtensions;
using CommandsRegistry.Application.Common.Exceptions;
using CommandsRegistry.Application.Common.Mappings;
using CommandsRegistry.Application.JsonCommands.Queries;

namespace CommandsRegistry.Infrastructure.JsonCommands.CommandEntries
{
    public class CommandsEntriesRepository: ICommandsEntriesRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CommandsEntriesRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommandEntry> CreateAsync(string name, string jsonSchema)
        {
            CommandEntry entity = new()
            {
                Name = name,
                JsonSchema = jsonSchema
            };

            await _context.CommandEntries.AddAsync(entity);
            await _context.SaveChangesAsync(CancellationToken.None);

            return entity;
        }

        public async Task<CommandEntry> GetAsync(int id, bool expectToFind = false)
        {
            var entity = await _context.CommandEntries
                .SingleOrDefaultAsync(x => x.Id == id);

            if (expectToFind && entity == null)
                throw new NotFoundException(typeof(CommandEntry).ToString(), id);

            return entity;
        }

        public async Task<IEnumerable<CommandEntryDto>> GetListAsync()
        {
            var result = await _context.CommandEntries
                .ProjectTo<CommandEntryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        public async Task<PaginatedList<CommandEntryDto>> GetListAsync(Pager pager, GetPaginatedJsonCommandsFilterModel filter = null)
        {
            var predicate = PredicateBuilder.New<CommandEntry>(true);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                    predicate.And(x => x.Name.Contains(filter.Name));
            }

            var result = await _context.CommandEntries
                .Where(predicate)
                .ProjectTo<CommandEntryDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(pager);

            return result;
        }

        public async Task Update(CommandEntryDto model)
        {
            var entity = await GetAsync(model.Id, true);
            _mapper.Map(model, entity);
            _context.CommandEntries.Update(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task Delete(int id)
        {
            var entity = await GetAsync(id, true);
            _context.CommandEntries.Remove(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
