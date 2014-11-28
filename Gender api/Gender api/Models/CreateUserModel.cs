using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gender_api.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Not a valid emal")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Not a valid emal")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(255, ErrorMessage = "Password have to be minimum 5 characters long", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Paswords dont match")]
        [StringLength(255, ErrorMessage = "Password have to be minimum 5 characters long", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Repeat password")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Please enter a your first name")]
        [Display(Name = "Fornavn")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Efternavn")]
        public string Lastname { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }
    }
}