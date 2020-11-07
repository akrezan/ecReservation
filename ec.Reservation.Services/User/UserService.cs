using System;
using System.Collections.Generic;
using System.Linq;
using ec.Reservation.Entities;
using System.Data.Entity;

namespace ec.Reservation.Services.User
{
    public class UserService
    {
        private Entities.Reservation resernationService = new Entities.Reservation();
        public List<Entities.User> GetAllUsers()
        {
            using (var context = new EntitiesContext())
            {
                return context.Users.ToList();

            }
        }

        public void RemovePollsForUser(int id)
        {
            using (var db = new EntitiesContext())
            {
                var polls = db.Polls.Where(u => u.UserId == id);

                if (polls != null)
                {
                    foreach (var poll in polls)
                    {
                        db.Polls.Remove(poll);
                    }
                    db.SaveChanges();

                }

            }
        }

        public void Remove(int id)
        {
            using (var db = new EntitiesContext())
            {

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var allReservationIds = db.Reservations.Select(x => x.Id);
                Entities.User user = db.Users.Find(id);

                var reservations = db.Reservations.Include(x=>x.Attendees);
                
                foreach (var reservation in reservations)
                {
                    reservation.RemoveAttendee(user);
                }

                ////// RemoveReservationFromUser

                var reservationFromDb = db.Reservations.Where(r => r.CreatorId == id).ToList();
                if (reservationFromDb != null)
                {
                    foreach (var reservation in reservationFromDb)
                    {
                        reservation.DeletePolls();
                        reservation.ClearAttendees();
                        db.Reservations.Remove(reservation);

                    }
                }

                db.Users.Remove(user);

                db.SaveChanges();
            }
        }


        public void RemoveReservationFromUser(int id)
        {
            using (var db = new EntitiesContext())
            {
                var reservationFromDb = db.Reservations.Where(r => r.CreatorId == id).ToList();
                if (reservationFromDb != null)
                {
                    foreach (var reservation in reservationFromDb)
                    {
                        reservation.DeletePolls();
                        reservation.ClearAttendees();
                        db.Reservations.Remove(reservation);
                        db.SaveChanges();
                    }

                   

                }

                db.SaveChanges();

            }
        }
    }
}
