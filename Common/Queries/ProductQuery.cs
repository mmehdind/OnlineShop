namespace OnlineShop.Common.Queries;

public class ProductQuery
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public int? CategoryId { get; set; }

    public string? Sort { get; set; }
}