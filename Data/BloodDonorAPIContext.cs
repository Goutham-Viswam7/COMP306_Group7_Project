using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BloodDonorAPI.Models;

namespace BloodDonorAPI.Data
{
    public class BloodDonorAPIContext : DbContext
    {
        public BloodDonorAPIContext (DbContextOptions<BloodDonorAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BloodDonorAPI.Models.Donors> Donors { get; set; }
    }
}
