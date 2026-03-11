using System;
using System.Collections.Generic;
using System.Text;
using UserAuthApiProperArchitecture.Application.DTOs;

namespace UserAuthApiProperArchitecture.Application.Interfaces
{
    // Job description for the User Service — the business logic layer 
    public class IUserService
    {
        Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO request) ;
        Task<UserResponseDTO> LoginAsync(LoginRequestDTO request);
    }
}
