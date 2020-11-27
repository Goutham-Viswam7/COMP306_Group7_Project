using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.Models
{
    public class DonorsData
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string BloodGroup { get; set; }

        public string Address { get; set; }
    }
}
