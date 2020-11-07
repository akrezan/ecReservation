using ec.Reservation.Clients.Web.Controllers;
using ec.Reservation.Clients.Web.Helpers;
using ec.Reservation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ec.Reservation.Clients.Web
{
    [ApiKeyAuthorize]
    public class ReservationApiController : ApiController
    {
        
        // GET api/<controller>/5
        public JsonResult Get(int id)
        {
            using (EntitiesContext dc = new EntitiesContext())
            {
                var reservations = (from r in dc.Reservations
                                    where r.ResourceId == id
                                    join u in dc.Users
                                        on r.CreatorId equals u.Id

                                    join rec in dc.Resources
                                        on r.ResourceId equals rec.Id
                                    select new
                                    {
                                        ID = r.Id,
                                        Name = r.Name,
                                        StartTime = r.StartTime,
                                        EndTime = r.EndTime,
                                        Creator = u.FirstName,
                                        Resource = rec.Name
                                    }).ToList();

                if (id < 0)
                {
                    reservations = (from r in dc.Reservations
                                    join u in dc.Users
                                        on r.CreatorId equals u.Id

                                    join rec in dc.Resources
                                        on r.ResourceId equals rec.Id
                                    select new
                                    {
                                        ID = r.Id,
                                        Name = r.Name,
                                        StartTime = r.StartTime,
                                        EndTime = r.EndTime,
                                        Creator = u.FirstName,
                                        Resource = rec.Name
                                    }).ToList();

                }

                var events = new List<Event>();

                foreach (var reservation in reservations)
                {
                    var eventViewModel = new Event()
                    {
                        EventID = reservation.ID,
                        Subject = reservation.Name,
                        Description = reservation.Resource,
                        Creator = reservation.Creator,
                        IsFullDay = false,
                        ThemeColor = "#9c2525",
                        End = reservation.EndTime,
                        Start = reservation.StartTime
                    };
                    events.Add(eventViewModel);
                }
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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