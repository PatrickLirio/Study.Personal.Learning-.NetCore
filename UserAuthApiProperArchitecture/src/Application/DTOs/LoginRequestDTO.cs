using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserAuthApiProperArchitecture.Application.DTOs
{
    // Only email + password needed to log in 

    public class LoginRequestDTO

    {

        [Required]
        [EmailAddress]

        public string Email { get; set; } = string.Empty;



        [Required]

        public string Password { get; set; } = string.Empty;

    }
}
