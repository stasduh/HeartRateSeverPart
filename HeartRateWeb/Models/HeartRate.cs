using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeartRateWeb.Models
{
    public class HeartRate
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Дата/Время")]
        public DateTime DateTime { get; set; }

        [Display(Name = "SportsmanId")]
        public string SportsmanId { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "ЧСС")]
        public string HeartRateValue { get; set; }
    }
}