using HotelListing.Api.Data;

namespace HotelListing.Api.Contracts
{
    public interface IHotelsRepository : IGenericRepository<Hotel>
    {
        Task<Hotel> GetHotelById(int id);
    }
}
