using ec.Reservation.Clients.Web.Helpers;
using ec.Reservation.Entities;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace ec.Reservation.Clients.Web
{
    [ApiKeyAuthorize]
    public class RessourceApiController : ApiController
    {
        // GET api/<controller>/5
        public JsonResult Get(int id)
        {
            if (id < 0)
            {
                return new JsonResult { Data = HttpStatusCode.BadRequest };
            }

            using (EntitiesContext dc = new EntitiesContext())
            {
                Resource resource = dc.Resources.Find(id);
                if (resource == null)
                {
                    return new JsonResult { Data = "Ressource Not Found" };
                }
                return new JsonResult { Data = resource };
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