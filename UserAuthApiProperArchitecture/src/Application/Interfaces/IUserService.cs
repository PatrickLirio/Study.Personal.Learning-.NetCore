
using UserAuthApiProperArchitecture.Application.DTOs;

namespace UserAuthApiProperArchitecture.Application.Interfaces
{
    // Job description for the User Service — the business logic layer 
    public interface IUserService
    {
        Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO request) ;
        Task<UserResponseDTO> LoginAsync(LoginRequestDTO request);
    }
}
