//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mijem
{
    using System;
    using System.Collections.Generic;
    
    public class Reservation
    {
        public int reservation_id { get; set; }
        public System.DateTime reservation_date { get; set; }
        public int contact_id { get; set; }
        public string description { get; set; }
    
        public virtual contact contact { get; set; }
    }
}
