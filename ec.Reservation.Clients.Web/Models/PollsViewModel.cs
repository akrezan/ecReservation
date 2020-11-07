using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ec.Reservation.Entities;

namespace ec.Reservation.Clients.Web.Models
{
    public class PollsViewModel : EntityViewModelBase
    {
        public PollAnswer Answer { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Comment { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ReservationId { get; set; }
        public virtual Entities.Reservation Reservation { get; set; }

        
    }
    public enum PollAnswer
    {
        Yes,
        No,
        NotAnswered

    }
}