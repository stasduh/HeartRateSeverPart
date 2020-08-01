using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeartRateWeb.Models
{
    public class Sportsman
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        
        [Display(Name = "Номер")]
        public string TeamNumber { get; set; }
    }
}