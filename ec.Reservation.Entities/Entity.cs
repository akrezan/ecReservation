using System;

namespace ec.Reservation.Entities
{
    public class Entity
    {
        public long Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}