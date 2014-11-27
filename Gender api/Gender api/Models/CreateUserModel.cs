using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gender_api.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Indtast venligst en email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Indtast venligst en gyldig email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Indtast venligst et kodeord")]
        [StringLength(255, ErrorMessage = "Kodeord skal være minimum 5 tegn", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Kodeord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Kodeord matcher ikke")]
        [StringLength(255, ErrorMessage = "Kodeord skal være minimum 5 tegn", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Gentag kodeord")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Indtast venligst et navn")]
        [Display(Name = "Fornavn")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Indtast venligst et efternavn")]
        [Display(Name = "Efternavn")]
        public string Lastname { get; set; }

        [Display(Name = "Firma navn")]
        public string Company { get; set; }
    }
}