using AutoMapper;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesRepository countriesRepository, IMapper mapper)
        {
            _countriesRepository = countriesRepository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var country = _mapper.Map<IEnumerable<GetCountryDto>>(await _countriesRepository.GetAllAsync());
            return Ok(country);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryIdDto>> GetCountry(int id)
        {
            var country = _mapper.Map<GetCountryIdDto>(await _countriesRepository.GetById(id));
            //_context.Countries.Include(p => p.Hotels).FirstOrDefaultAsync(q => q.CountryId == id)

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, GetCountryDto country)
        {
            if (id != country.CountryId)
            {
                return BadRequest();
            }

            //_context.Entry(country).State = EntityState.Modified;

            var countryItem = await _countriesRepository.GetByIdAsync(id); //_context.Countries.FindAsync(id);
            if (countryItem == null)
            {
                return NotFound();
            }
            _mapper.Map(country, countryItem);
            await _countriesRepository.UpdateAsync(countryItem);
            //_mapper.Map(country, countryItem);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CountryExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);

            await _countriesRepository.CreateAsync(country);
            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //var country = await _countriesRepository.GetByIdAsync(id);
            //_context.Countries.FindAsync(id);
            //if (country == null)
            //{
            //    return NotFound();
            //}

            await _countriesRepository.DeleteAsync(id);

            //_context.Countries.Remove(country);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        //private async bool CountryExists(int id)
        //{
        //    return await _countriesRepository.Exists(id);
        //    //_context.Countries.Any(e => e.CountryId == id);
        //}
    }
}
