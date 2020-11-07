using ec.Reservation.Clients.Web.Helpers;
using ec.Reservation.Entities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace ec.Reservation.Clients.Web
{
    [ApiKeyAuthorize]
    public class UserApiController : ApiController
    {
        // GET api/<controller>/5
        public JsonResult Get(int id)
        {
            if (id < 0)
            {
                return new JsonResult { Data = HttpStatusCode.BadRequest };
            }

            using (EntitiesContext ec = new EntitiesContext())
            {
                User userFromDb = ec.Users.Find(id);
                if (userFromDb == null)
                {
                    return new JsonResult { Data = "User Not Found" };
                }
                var pollsOfThisUser = new List<Poll>();
                var reservationOfThisUser = new List<ec.Reservation.Entities.Reservation>();

                foreach(var poll in userFromDb.Polls)
                {
                    var pollToAdd = new Poll()
                    {
                        Id = poll.Id,
                        Answer = poll.Answer,
                        Comment = poll.Comment,
                        
                    };

                    pollsOfThisUser.Add(pollToAdd);
                }

                foreach (var reservation in userFromDb.Reservations)
                {
                    var reservationToAdd = new ec.Reservation.Entities.Reservation()
                    {
                        Id = reservation.Id,
                        Name = reservation.Name,
                        CreatorId = reservation.CreatorId,
                        
                    };

                    reservationOfThisUser.Add(reservationToAdd);
                }

                var userObjectToReturnToTheWorld = new User()
                {
                    FirstName = userFromDb.FirstName,
                    LastName = userFromDb.LastName,
                    Email = userFromDb.Email,
                    Id = userFromDb.Id,
                    UserType = userFromDb.UserType,
                    UserName = userFromDb.UserName,
                    CreationDateTime = userFromDb.CreationDateTime,
                    ModifiedDateTime = userFromDb.ModifiedDateTime,
                    Polls = pollsOfThisUser,
                    Reservations = reservationOfThisUser
                };

                return new JsonResult { Data = userObjectToReturnToTheWorld };
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}