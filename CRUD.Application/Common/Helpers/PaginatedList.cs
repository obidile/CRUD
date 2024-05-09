using Microsoft.EntityFrameworkCore;

namespace Craft.Application.Common.Helpers;

public class PaginatedList<T> : List<T>
{
    public List<T> Items { get; set; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public long TotalCount { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedList(long totalCount, int pageNumber, int totalPages)
    {
        PageNumber = pageNumber;
        TotalPages = totalPages;
        TotalCount = totalCount;
    }
    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        : base(items)
    {
        PageNumber = pageNumber;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
