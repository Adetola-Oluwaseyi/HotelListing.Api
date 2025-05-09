using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        private readonly HotelListingDbContext _context;
        public HotelsRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Hotel> GetHotelById(int id)
        {
            if (id == null)
                return null;
            return _context.Hotels.Include(p => p.Country).FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
