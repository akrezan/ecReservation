using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ec.Reservation.Clients.Web.Models
{
    public class ResourcesViewModel : EntityViewModelBase
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Code")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Code { get; set; }
        public Entities.UserType UserType { get; set; }
    }
}