using System.Collections.Generic;

namespace ec.Reservation.Entities
{
    public class User: Entity
    {
       
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Poll> Polls { get; set; }

    }

    public enum UserType
    {
        Administrator,
        NormalUser
    }
}
