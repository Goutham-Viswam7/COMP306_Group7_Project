using BloodDonorAPI.Data;
using BloodDonorAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonorAPI.Controllers
{
    public class DonorRepository : IDonorRepository
    {
        private readonly BloodDonorAPIContext bloodDonorAPIContext;

        public DonorRepository(BloodDonorAPIContext bloodDonorAPIContext)
        {
            this.bloodDonorAPIContext = bloodDonorAPIContext;
        }

        public async Task<IEnumerable<Donors>> GetDonors()
        {
            return await bloodDonorAPIContext.Donors.ToListAsync();
        }

        public async Task<Donors> GetDonor(int Id)
        {
            return await bloodDonorAPIContext.Donors
                .FirstOrDefaultAsync(d => d.Id == Id);
        }

        public async Task<Donors> AddDonor(Donors donor)
        {
            var result = await bloodDonorAPIContext.Donors.AddAsync(donor);
            await bloodDonorAPIContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Donors> UpdateDonor(Donors donor)
        {
            var result = await bloodDonorAPIContext.Donors
                .FirstOrDefaultAsync(d => d.Id == donor.Id);

            if (result != null)
            {
                result.Name = donor.Name;
                result.Mobile = donor.Mobile;
                result.Email = donor.Email;
                result.Age = donor.Age;
                result.BloodGroup = donor.BloodGroup;
                result.Address = donor.Address;

                await bloodDonorAPIContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Donors> DeleteDonor(int Id)
        {
            var result = await bloodDonorAPIContext.Donors
                .FirstOrDefaultAsync(d => d.Id == Id);
            if (result != null)
            {
                bloodDonorAPIContext.Donors.Remove(result);
                await bloodDonorAPIContext.SaveChangesAsync();
            }
            return null;
        }
    }
}
