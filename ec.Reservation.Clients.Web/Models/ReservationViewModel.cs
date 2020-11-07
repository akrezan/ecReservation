using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ec.Reservation.Entities;

namespace ec.Reservation.Clients.Web.Models
{
    public class ReservationViewModel : EntityViewModelBase
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        
        public Resource Resource { get; set; }
        
        public User Creator { get; set; }
        public ICollection<CheckList> CheckLists { get; set; }
        public ICollection<User> Attendees { get; set; }
        public ICollection<Poll> Polls { get; set; }

        [Required]
        public long ResourceId { get; set; }

        [Required]
        public long CreatorId { get; set; }
        public ICollection<long> ChecklistIds { get; set; }
        public ICollection<long> AttendeeIds { get; set; }
        public ICollection<long> PollIds { get; set; }
    }
}