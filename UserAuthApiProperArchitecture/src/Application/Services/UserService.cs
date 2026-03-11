
using UserAuthApiProperArchitecture.Application.DTOs;
using UserAuthApiProperArchitecture.Application.Interfaces;
using UserAuthApiProperArchitecture.Domain.Entities;
using UserAuthApiProperArchitecture.Domain.Enums;

namespace UserAuthApiProperArchitecture.Application.Services
{
    public class UserService : IUserService
    {
        // These are INTERFACES — we don't care about the concrete implementation 
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        // Constructor Injection — .NET DI container provides these 
        public UserService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO request)

        {
            // Step 1: Business rule — email must be unique 
            if (await _userRepository.ExistsByEmailAsync(request.Email))
                throw new InvalidOperationException("Email already in use.");
            if (await _userRepository.ExistsByUsernameAsync(request.Username))
                throw new InvalidOperationException("Username already taken.");
            // Step 2: Create entity — hash the password before saving 
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.ToLowerInvariant(),
                Username = request.Username,
                // BCrypt.HashPassword turns plain text into a secure hash 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow
            };
            // Step 3: Persist to database via repository    
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            // Step 4: Generate JWT and return response DTO 
            var token = _jwtTokenService.GenerateToken(user);
            return MapToResponseDTO(user, token);
        }

        public async Task<UserResponseDTO> LoginAsync(LoginRequestDTO request)

        {

            // Step 1: Find user by email 

            var user = await _userRepository.GetByEmailAsync(request.Email.ToLowerInvariant())

                ?? throw new UnauthorizedAccessException("Invalid email or password.");



            // Step 2: Verify password against stored hash 

            // BCrypt.Verify is timing-safe — prevents timing attacks 

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))

                throw new UnauthorizedAccessException("Invalid email or password.");



            // Step 3: Generate JWT token 

            var token = _jwtTokenService.GenerateToken(user);

            return MapToResponseDTO(user, token);

        }



        // Private helper — maps User entity to UserResponseDTO 
        private static UserResponseDTO MapToResponseDTO(User user, string token) => new()

        {

            Id = user.Id,

            FirstName = user.FirstName,

            LastName = user.LastName,

            Email = user.Email,

            Username = user.Username,

            Role = user.Role.ToString(),

            Token = token

        };

    } 
}
