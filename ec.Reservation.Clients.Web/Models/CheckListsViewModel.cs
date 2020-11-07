using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ec.Reservation.Clients.Web.Models
{
    public class CheckListsViewModel : EntityViewModelBase
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        

        [Required]
        [Display(Name = "Details")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Details { get; set; }
        public Entities.UserType UserType { get; set; }
    }
}