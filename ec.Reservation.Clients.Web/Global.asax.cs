using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using ec.Reservation.Clients.Web.Models;
using ec.Reservation.Entities;
using System.Web.Http;

namespace ec.Reservation.Clients.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureMapping();
        }

        private void ConfigureMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>().ReverseMap();
                cfg.CreateMap<Resource, ResourcesViewModel>();
                cfg.CreateMap<CheckList, CheckListsViewModel>();
                cfg.CreateMap<Poll, PollsViewModel>().ReverseMap();
                cfg.CreateMap<Entities.Reservation, ReservationViewModel>().ReverseMap();
            });

        }

    }

    
}
