//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gender_api.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class keys
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int requests { get; set; }
        public int requests_allowed { get; set; }
        public string key { get; set; }
    
        public virtual users users { get; set; }
    }
}
