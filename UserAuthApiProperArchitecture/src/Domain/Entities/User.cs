using UserAuthApiProperArchitecture.Domain.Enums;

namespace UserAuthApiProperArchitecture.Domain.Entities
{
    // This class represents a USER in our system. 
    // Each property maps to a column in the database. 
    public class User
    {
        // Guid = Globally Unique Identifier (e.g., 3fa85f64-5717-4562-b3fc-2c963f66afa6) 
        // Better than int IDs for security — attackers can't guess the next user ID 
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Email is used for login
        public string Email { get; set; } = string.Empty;

        // Username is a display name 
        public string Username { get; set; } = string.Empty;

        // NEVER store the real password! Only store the hash. 
        // BCrypt turns 'myPassword123' into '$2a$11$...' (unreadable gibberish) 
        public string PasswordHash { get; set; } = string.Empty;

        // UTC timestamp so time zones don't cause problems 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Which role does this user have? Stored as int (1 or 2) in the DB 
        public UserRole Role { get; set; } = UserRole.User;
    }
}
