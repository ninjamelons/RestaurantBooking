using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace UserWebClient.Models
{
    public class RegisterViewModel
    {
        public Customer Customer { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name="Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}