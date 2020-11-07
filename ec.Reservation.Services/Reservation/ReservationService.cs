using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ec.Reservation.Entities;
using ec.Reservation.Services.Helper;
using System.Configuration;

namespace ec.Reservation.Services.Reservation
{
    public class ReservationService
    {
       
        public void CreateReservation(Entities.Reservation reservation, ICollection<long> attendeeIds, ICollection<long> checklistIds)
        {
            using (var db = new EntitiesContext())
            {
                reservation.CreationDateTime = DateTime.Now;
                if (checklistIds != null)
                {
                    reservation.CheckLists = db.CheckLists.Where(u => checklistIds.Contains(u.Id)).ToList();
                }

                if (attendeeIds != null)
                {
                    var emailService = new EmailService();

                    foreach (var user in db.Users.Where(u => attendeeIds.Contains(u.Id)))
                    {
                        reservation.AddAttendee(user);
                       var userEmail = user.Email;
                       
                        emailService.SendReservationNotificationEmail(reservation.CreatorFullName, reservation.Name,reservation.StartTime,reservation.EndTime, userEmail, ConfigurationManager.AppSettings["host"]);
                    }
                }


                db.Reservations.Add(reservation);
                db.SaveChanges();
            }
        }

        public void UpdateReservation(Entities.Reservation newReservation)
        {
            using (var db = new EntitiesContext())
            {
                var reservationFromDb = db.Reservations.SingleOrDefault(x => x.Id == newReservation.Id);

                reservationFromDb.Name = newReservation.Name;
                reservationFromDb.CreatorId = newReservation.CreatorId;
                reservationFromDb.ResourceId = newReservation.ResourceId;
                reservationFromDb.StartTime = newReservation.StartTime;
                reservationFromDb.EndTime = newReservation.EndTime;
                reservationFromDb.CreationDateTime = newReservation.CreationDateTime;
                reservationFromDb.ModifiedDateTime = DateTime.Now;

                if (newReservation.ChecklistIds != null)
                {
                    var checklistToRemove =
                        reservationFromDb.CheckLists.Where(x => !newReservation.ChecklistIds.Contains(x.Id)).ToList();

                    foreach (var checklist in checklistToRemove)
                    {
                        reservationFromDb.CheckLists.Remove(checklist);
                    }
                
                    var checklistIdToAdd =
                        newReservation.ChecklistIds.Where(x =>
                            !reservationFromDb.CheckLists.Select(c => c.Id).Contains(x)).ToList();

                    var checklistToAdd = db.CheckLists.Where(x => checklistIdToAdd.Contains(x.Id)).ToList();
                    foreach (var checklist in checklistToAdd)
                    {
                        reservationFromDb.CheckLists.Add(checklist);
                    }
                }
                else
                {
                    reservationFromDb.CheckLists.Clear();
                }



                if (newReservation.AttendeeIds != null)
                {
                    var attendeesToRemove =
                    reservationFromDb.Attendees.Where(x => !newReservation.AttendeeIds.Contains(x.Id)).ToList();

                foreach (var attendee in attendeesToRemove)
                {
                    reservationFromDb.RemoveAttendee(attendee);
                }

                
                    var attendeesIdToAdd =
                        newReservation.AttendeeIds.Where(x =>
                            !reservationFromDb.Attendees.Select(u => u.Id).Contains(x)).ToList();
                    var attendeesToAdd = db.Users.Where(u => attendeesIdToAdd.Contains(u.Id)).ToList();

                    foreach (var attendee in attendeesToAdd)
                    {
                        reservationFromDb.AddAttendee(attendee);
                    }

                }
                else
                {
                    foreach (var attendee in reservationFromDb.Attendees)
                    {
                        var poll = db.Polls.FirstOrDefault(r => r.ReservationId == reservationFromDb.Id && r.UserId == attendee.Id);

                        if (poll != null)
                        {
                            db.Polls.Remove(poll);
                        }

                    }
                    reservationFromDb.Attendees.Clear();
                    
                }

                db.SaveChanges();
            }
        }


    }
}
