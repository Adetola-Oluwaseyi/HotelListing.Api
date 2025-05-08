namespace HotelListing.Api.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task<bool> Exists(int? id);

    }
}
