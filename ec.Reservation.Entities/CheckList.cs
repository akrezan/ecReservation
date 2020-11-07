using System.Collections.Generic;

namespace ec.Reservation.Entities
{
   public class CheckList: Entity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
