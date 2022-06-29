using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.DTOs.Countries;
using AutoMapper;
using HotelListing.API.IRepository;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository countryRepo;
        private readonly IMapper mapper;

        public CountriesController(ICountryRepository countryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            countryRepo = countryRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await countryRepo.GetAllAsync();
            var records = mapper.Map<List<GetCountriesDTO>>(countries);

            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await countryRepo.GetDetails(id);        

            if (country == null)
            {
                return NotFound();
            }

            var countryDTO = mapper.Map<CountryDTO>(country);

            return Ok(countryDTO);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountry)
        {
            if (id != updateCountry.Id)
            {
                return BadRequest();
            }

            var country = await countryRepo.GetAsync(id);
            if(country  == null)
            {
                return NotFound();
            }

            mapper.Map(updateCountry, country);

            try
            {
                await countryRepo.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await countryRepo.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountry)
        {
            var country = mapper.Map<Country>(createCountry);

            await countryRepo.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (countryRepo.GetAllAsync() == null)
            {
                return NotFound();
            }
            var country = await countryRepo.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await countryRepo.DeleteAsync(id);

            return NoContent();
        }


    }
}
