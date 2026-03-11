
using System.ComponentModel.DataAnnotations;


namespace UserAuthApiProperArchitecture.Application.DTOs
{
    // This DTO is what the client SENDS when registering.
    // Notice: no Id, no PasswordHash, no CreatedAt — client doesn't control those. 

    public class RegisterRequestDTO

        {

            [Required(ErrorMessage = "First name is required")]

            [MaxLength(100)]

            public string FirstName { get; set; } = string.Empty;



            [Required(ErrorMessage = "Last name is required")]

            [MaxLength(100)]

            public string LastName { get; set; } = string.Empty;



            [Required(ErrorMessage = "Email is required")]

            [EmailAddress(ErrorMessage = "Invalid email format")]

            public string Email { get; set; } = string.Empty;



            [Required(ErrorMessage = "Username is required")]

            [MinLength(3), MaxLength(100)]

            public string Username { get; set; } = string.Empty;



            [Required(ErrorMessage = "Password is required")]

            [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]

            public string Password { get; set; } = string.Empty;

        }
}
