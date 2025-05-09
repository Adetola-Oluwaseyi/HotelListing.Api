//using HotelListing.Api.Data;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace HotelListing.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class HotelController : ControllerBase
//    {
//        private static List<Hotel> hotels = new List<Hotel>()
//        {
//            new Hotel(){Id =  1, Name = "Eko Hotel", Address = "Victoria Island, Lagos, Nigeria", Rating = 4.7},
//            new Hotel() {Id = 2, Name = "Toscana", Address = "Oniru, Victoria Island, Lagos, Nigeria", Rating = 4.5}
//        };
//        // GET: api/<HotelController>
//        [HttpGet]
//        public ActionResult<IEnumerable<Hotel>> Get()
//        {
//            return Ok(hotels);
//        }

//        // GET api/<HotelController>/5
//        [HttpGet("{id}")]
//        public ActionResult<Hotel> Get(int id)
//        {
//            var hotel = hotels.Find(hotel => hotel.Id == id);
//            if (hotel == null)
//                return NotFound();
//            return Ok(hotel);
//        }

//        // POST api/<HotelController>
//        [HttpPost]
//        public ActionResult<Hotel> Post([FromBody] Hotel newHotel)
//        {
//            if (hotels.Any(hotel => hotel.Id == newHotel.Id))
//            {
//                return BadRequest("Hotel with this Id already exists!");
//            }
//            hotels.Add(newHotel);
//            return CreatedAtAction(nameof(Get), new { Id = newHotel.Id }, newHotel);
//        }

//        // PUT api/<HotelController>/5
//        [HttpPut("{id}")]
//        public ActionResult<Hotel> Put(int id, [FromBody] Hotel value)
//        {
//            var h = hotels.FirstOrDefault(h => h.Id == id);
//            if (h == null)
//                return NotFound(new { message = "Hotel with this ID not found" });
//            h.Name = value.Name;
//            h.Address = value.Address;
//            h.Rating = value.Rating;

//            return NoContent();
//        }


//        // DELETE api/<HotelController>/5
//        [HttpDelete("{id}")]
//        public ActionResult Delete(int id)
//        {
//            var h = hotels.FirstOrDefault(h => h.Id == id);
//            if (h == null)
//                return NotFound(new { message = "Hotel with this ID not found" });
//            hotels.Remove(h);
//            return NoContent();
//        }
//    }
//}
