using OnlineShop.Models.Base;
using OnlineShop.Models.Identity;

namespace OnlineShop.Models;

public class Address : BaseEntity
{
    public string AppUserId { get; set; } = string.Empty;

    public AppUser AppUser { get; set; } = null!;

    public string Title { get; set; } = string.Empty;

    public string RecipientName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Province { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string FullAddress { get; set; } = string.Empty;

    public bool IsDefault { get; set; }
}