using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mijem.Models
{
    public class ContactMetadata
    {
        public int contact_id { get; set; }
        [StringLength(50)]
        [Display(Name = "Contact Name")]
        public string name { get; set; }
        [StringLength(16)]
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime birth_date { get; set; }
        public int contact_type_id { get; set; }
    }

    public class ReservationMetaData
    {
        public int reservation_id { get; set; }
        [Display(Name = "Reservation  Date")]
        public System.DateTime reservation_date { get; set; }
        [Display(Name = "Contact Name")]
        public int contact_id { get; set; }
        public string description { get; set; }

    }

}