using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ec.Reservation.Entities
{
    public class Reservation : Entity
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long ResourceId { get; set; }
        public long CreatorId { get; set; }
        public ICollection<long> ChecklistIds { get; set; }
        public ICollection<long> AttendeeIds { get; set; }
        public virtual Resource Resource { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<CheckList> CheckLists { get; set; }

        public virtual ICollection<User> Attendees { get; set; }
        public virtual ICollection<Poll> Polls { get; set; }

        [NotMapped]
        public string CreatorFullName { get; set; }

        public void AddAttendee(User attendee)
        {
            Attendees.Add(attendee);

            Polls.Add(new Poll
            {
                User = attendee,
                CreationDateTime = DateTime.Now,
                Answer = PollAnswer.NotAnswered
            });
        }

        public void RemoveAttendee(User attendee)
        {
            using (var db = new EntitiesContext())
            {
                RemovePollsForAttendee(attendee);
                if (Attendees != null)
                {
                    Attendees.Remove(attendee);
                }

                db.SaveChanges();
            }

        }

        public void RemovePollsForAttendee(User attendee)
        {
            using (var db = new EntitiesContext())
            {
                var poll = db.Polls.FirstOrDefault(u => u.ReservationId == Id && u.UserId == attendee.Id);

                if (poll != null)
                {
                    db.Polls.Remove(poll);

                    db.SaveChanges();
                }

               
            }
        }


        public void ClearAttendees()
        {
            Attendees.Clear();
        }

        public void DeletePolls()
        {
            using (var db = new EntitiesContext())
            {
                var polls = db.Polls.Where(u => u.ReservationId == Id);

                foreach (var poll in polls)
                {
                    db.Polls.Remove(poll);
                }
                db.SaveChanges();
            }
        }


    }
}
