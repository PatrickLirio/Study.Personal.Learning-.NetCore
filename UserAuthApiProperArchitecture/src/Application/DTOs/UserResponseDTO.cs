using System;
using System.Collections.Generic;
using System.Text;

namespace UserAuthApiProperArchitecture.Application.DTOs
{
    // This is what we RETURN to the client after login/register. 
    // Notice: NO PasswordHash field — we never send that back! 
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // JWT token to return after login/register
    }
}
