using System.Collections.Generic;

namespace ec.Reservation.Entities
{
   public class Poll: Entity
    {
        public PollAnswer Answer { get; set; }
        public string Comment { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }


    }
   public enum PollAnswer
   {
        Yes,
        No,
        NotAnswered
   }
}
