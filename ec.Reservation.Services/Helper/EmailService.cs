using FluentEmail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ec.Reservation.Services.Helper
{
    public class EmailService
    {
        public void SendReservationNotificationEmail(string creator, string reservationName, DateTime startDate, DateTime endDate, string emailAddress, string host)
        {
            var url = string.Format("{0}/Polls", host);
            var message = string.Format("Sie haben einladung zur Reservation : {0} ab: {1} bis {2} von {3} bekommen, drücken Sie bitte hier : {4}", reservationName, startDate, endDate, creator, url);

            Console.WriteLine("Email send to " + emailAddress);
            var email = Email
                .From("reservationSystem@e-consult.de")
                .To(emailAddress)
                .Subject("Neue Reservation")
                .Body(message);

               email.Send();


        }

        public void SendNewUserNotificationEmail(string creator, string host)
        {
            var url = string.Format("{0}/Users", host);
            var message = string.Format("Wichtig: {0} Hat sich als neuer User Angemeldet,um weitere Infos zu sehen drücken Sie bitte hier : {1}", creator, url);

            
            var email = Email
                .From("reservationSystem@e-consult.de")
                .To(ConfigurationManager.AppSettings["AdminEmail"])
                .Subject("Neuer User")
                .Body(message);

            email.Send();


        }
    }
}
