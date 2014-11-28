using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gender_api.Models
{
    public class CreateKeyModel
    {
        [Required]
        public int KeyLimit { get; set; }

        public int SelectedItemId { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }

    }
}