using AutoMapper;
using OnlineShop.DTOs.Address;
using OnlineShop.Models;
using OnlineShop.Repositories.Interfaces;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class AddressService : IAddressService
{
    private readonly IRepository<Address> _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public AddressService(
        IRepository<Address> repo,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUser,
        IMapper mapper)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<List<AddressDto>> GetUserAddressesAsync()
    {
        var addresses = await _repo.FindAsync(
            x => x.AppUserId == _currentUser.UserId);

        return _mapper.Map<List<AddressDto>>(addresses);
    }

    public async Task<AddressDto?> GetByIdAsync(int id)
    {
        var address =
            await _repo.GetByIdAsync(id);

        if (address == null)
            return null;

        if (address.AppUserId != _currentUser.UserId)
            return null;

        return _mapper.Map<AddressDto>(address);
    }

    public async Task CreateAsync(CreateAddressDto dto)
    {
        var address =
            _mapper.Map<Address>(dto);

        address.AppUserId =
            _currentUser.UserId;

        var hasAddress =
            (await _repo.FindAsync(
                x => x.AppUserId == _currentUser.UserId))
            .Any();

        if (!hasAddress)
            address.IsDefault = true;

        await _repo.AddAsync(address);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateAddressDto dto)
    {
        var address =
            await _repo.GetByIdAsync(dto.Id);

        if (address == null)
            return;

        if (address.AppUserId != _currentUser.UserId)
            return;

        address.Title = dto.Title;
        address.RecipientName = dto.RecipientName;
        address.PhoneNumber = dto.PhoneNumber;
        address.Province = dto.Province;
        address.City = dto.City;
        address.PostalCode = dto.PostalCode;
        address.FullAddress = dto.FullAddress;

        _repo.Update(address);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var address =
            await _repo.GetByIdAsync(id);

        if (address == null)
            return;

        if (address.AppUserId != _currentUser.UserId)
            return;

        bool wasDefault =
            address.IsDefault;

        _repo.Delete(address);

        await _unitOfWork.SaveChangesAsync();

        if (wasDefault)
        {
            var addresses =
                await _repo.FindAsync(
                    x => x.AppUserId == _currentUser.UserId);

            var first =
                addresses.FirstOrDefault();

            if (first != null)
            {
                first.IsDefault = true;

                _repo.Update(first);

                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

    public async Task SetDefaultAsync(int id)
    {
        var addresses =
            await _repo.FindAsync(
                x => x.AppUserId == _currentUser.UserId);

        foreach (var item in addresses)
        {
            item.IsDefault = item.Id == id;

            _repo.Update(item);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}