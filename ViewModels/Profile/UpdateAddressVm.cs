namespace OnlineShop.ViewModels.Profile;

public class UpdateAddressVm
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}