using BloodDonorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonorAPI.Controllers
{
    public interface IDonorRepository
    {
        Task<IEnumerable<Donors>> GetDonors();
        Task<Donors> GetDonor(int Id);
        Task<Donors> AddDonor(Donors donor);
        Task<Donors> UpdateDonor(Donors donor);
        Task<Donors> DeleteDonor(int Id);
    }
}
