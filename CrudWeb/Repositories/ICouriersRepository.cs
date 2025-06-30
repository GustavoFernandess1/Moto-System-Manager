using CrudWeb.Models;

namespace CrudWeb.Repositories
{
    public interface ICouriersRepository
    {
        Task<IEnumerable<CouriersModel>> GetAllAsync();
        Task<int> AddAsync(CouriersModel courier);
    }
}
