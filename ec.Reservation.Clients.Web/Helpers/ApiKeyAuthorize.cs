using System.Web.Http;
using AutoMapper;
using System.Web.Http.Controllers;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Configuration;

namespace ec.Reservation.Clients.Web.Helpers
{
    public class ApiKeyAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext httpActionContext)
        {
            var httpContext = httpActionContext;
            IEnumerable<string> values;
            var allKeys = httpActionContext.Request.Headers.TryGetValues("ApiKey", out values);

            if(values!=null && !string.IsNullOrEmpty( values.FirstOrDefault()))
            {
                if(values.FirstOrDefault()== ConfigurationManager.AppSettings["ValidApiKey"])
                {
                    return true;
                }
            }


            throw new Exception("Api Key not valid");

          

            return false;
        }

    }
}


