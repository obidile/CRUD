using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Craft.Application.Common.Helpers;

namespace CRUD.Application.Common.Helpers;

public static class MapperUtility
{
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
    {
        return PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
    }

    public static Task<List<TDestination>> ToPaginatedListAsync<TDestination>(this IQueryable queryAble, IConfigurationProvider configuration) where TDestination : class
    {
        return queryAble.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
    }

    public static PaginatedList<TDestination> ToPaginatedList<TSource, TDestination>(this IMapper mapper, PaginatedList<TSource> source) where TSource : class where TDestination : class
    {
        var data = mapper.Map<List<TDestination>>(source.Items);

        return new PaginatedList<TDestination>(source.TotalCount, source.PageNumber, source.TotalPages)
        {
            Items = data
        };
    }
}
