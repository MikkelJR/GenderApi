using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gender_api.Models
{
    public class LoginModel
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}