using AutoMapper;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
        {
            _hotelsRepository = hotelsRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotels()
        {
            return Ok(_mapper.Map<List<GetHotelsDto>>(await _hotelsRepository.GetAllAsync()));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelsIdDto>> GetHotel(int id)
        {

            var hotel = await _hotelsRepository.GetHotelById(id);

            if (hotel == null)
            {
                return NotFound();
            }
            var hotelDto = _mapper.Map<GetHotelsIdDto>(hotel);

            return Ok(hotelDto);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, GetHotelsDto hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }

            var hotel = await _hotelsRepository.GetByIdAsync(id);
            _mapper.Map(hotelDto, hotel);

            await _hotelsRepository.UpdateAsync(hotel);

            //_context.Entry(hotel).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotelDto)
        {
            if (hotelDto == null)
            {
                return NoContent();
            }
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _hotelsRepository.CreateAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotelDto);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        //private bool HotelExists(int id)
        //{
        //    return _context.Hotels.Any(e => e.Id == id);
        //}
    }
}
