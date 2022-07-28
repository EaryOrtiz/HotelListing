using HotelListing.API.Data;
using HotelListing.API.DTOs.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.IRepository
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDTO);

        Task<AuthResponseDTO> Login(LoginUserDTO loginUserDTO);

        Task<string> GenerateToken(ApiUser user);
    }
}
