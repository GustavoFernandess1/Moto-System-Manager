using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;

namespace CrudWeb.Interfaces
{
    public interface IMotorcycleService
    {
        public Task<MotorcycleResponse> RegisterMotorcycle(MotorcycleRegisterRequest request);
        Task<MotorcycleResponse> GetByLicensePlateAsync(string licensePlate);
        Task<MotorcycleResponse> UpdateLicensePlateAsync(int id, string licensePlate);
        Task<MotorcycleResponse> DeleteMotorcycleAsync(int id);
        Task<MotorcycleResponse> GetByIdAsync(int id);
    }
}