using OnlineShop.Models;

namespace OnlineShop.Common.Queries;

public class OrderQuery
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public OrderStatus? Status { get; set; }

    public string? Search { get; set; }   // username / order id

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }
}