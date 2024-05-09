namespace CRUD.Application.Common.Models;

public class SearchQueryParams : QueryParams
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
public class QueryParams : PagedParams
{
    public string SearchKeyword { get; set; } = string.Empty;
}
public class PagedParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

}