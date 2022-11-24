using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Models;

namespace CommandsRegistry.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable,
            int pageNumber,
            int pageSize)
        {
            return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
        }

        public async static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable,
            Pager pager)
            where TDestination : class
        {
            var paginatedQuery = queryable.Paginate(pager);
            var items = await paginatedQuery.ToListAsync();

            return new PaginatedList<TDestination>(items, pager.TotalRows, pager.Index, pager.Size);
        }

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
            this IQueryable queryable,
            IConfigurationProvider configuration)
        {
            return queryable
                .ProjectTo<TDestination>(configuration)
                .ToListAsync();
        }
    }
}
