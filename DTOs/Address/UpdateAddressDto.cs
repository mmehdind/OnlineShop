namespace OnlineShop.DTOs.Address;

public class UpdateAddressDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string RecipientName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Province { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string FullAddress { get; set; } = string.Empty;
}