﻿using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.DTOs.Users
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
