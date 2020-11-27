using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodDonorAPI.Data;
using BloodDonorAPI.Models;

namespace BloodDonorAPI.Controllers
{
    [Route("api/donors")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly IDonorRepository donorRepository;

        public DonorsController(IDonorRepository donorRepository)
        {
            this.donorRepository = donorRepository;
        }

        //private readonly BloodDonorAPIContext _context;

        //public DonorsController(BloodDonorAPIContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Donors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donors>>> GetDonors()
        {
            try
            {
                return Ok(await donorRepository.GetDonors());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/Donors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donors>> GetDonors(int id)
        {
            try
            {
                var result = await donorRepository.GetDonor(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // PUT: api/Donors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<Donors>> PutDonor(int id, Donors donors)
        {
            try
            {
                if (id != donors.Id)
                    return BadRequest("Donor ID mismatch");

                var donorToUpdate = await donorRepository.GetDonor(id);

                if (donorToUpdate == null)
                    return NotFound($"Donor with Id = {id} not found");

                return await donorRepository.UpdateDonor(donors);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // POST: api/Donor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Donors>> PostDonor(Donors donors)
        {
            try
            {
                if (donors == null)
                    return BadRequest();

                var createdDonor = await donorRepository.AddDonor(donors);

                return CreatedAtAction(nameof(GetDonors),
                    new { id = createdDonor.Id }, createdDonor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Donor");
            }
        }

        // DELETE: api/Donor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Donors>> DeleteDonor(int id)
        {
            try
            {
                var donorToDelete = await donorRepository.GetDonor(id);
                if (donorToDelete == null)
                {
                    return NotFound($"Donor with Id = {id} not found");
                }

                return await donorRepository.DeleteDonor(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
