namespace OnlineShop.ViewModels.Profile;

public class AddressVm
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
}