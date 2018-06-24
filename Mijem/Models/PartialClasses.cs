using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mijem.Models
{
   [MetadataType(typeof(ContactMetadata))]
    public partial class contact
    {
   }

    [MetadataType(typeof(ReservationMetaData))]
    public partial class reservation
    {
    }
}