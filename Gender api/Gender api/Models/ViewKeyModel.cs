using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Gender_api.Models
{
    public class ViewKeyModel
    {
        public string Key { get; set; }
        public int Requests { get; set; }
        public int RequestLimit { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
    }
}