using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ec.Reservation.Clients.Web.Models
{
    public class EntityViewModelBase
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}