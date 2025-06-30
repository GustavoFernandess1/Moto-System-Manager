using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Models;
using CrudWeb.Models.Request;

namespace CrudWeb.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<int> AddMotorcycleAsync(MotorcycleModel motorcycle);
        Task<MotorcycleModel> GetByLicensePlateAsync(string licensePlate);
        Task<MotorcycleModel> GetByIdAsync(int id);
        Task UpdateLicensePlateAsync(int id, string licensePlate);
        Task DeleteMotorcycleAsync(int id);
    }
}