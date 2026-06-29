using OnlineShop.DTOs.Address;

namespace OnlineShop.Services.Interfaces;

public interface IAddressService
{
    Task<List<AddressDto>> GetUserAddressesAsync();

    Task<AddressDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateAddressDto dto);

    Task UpdateAsync(UpdateAddressDto dto);

    Task DeleteAsync(int id);

    Task SetDefaultAsync(int id);
}