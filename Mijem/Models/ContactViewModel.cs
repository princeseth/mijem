using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Mijem.Models
{
    public class ContactViewModel
    {
        public int contact_id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public System.DateTime birth_date { get; set; }
        public int contact_type_id { get; set; }
        // custom attributes
        [AllowHtml]
        public string description { get; set; }
    }

}