using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Models;
using CrudWeb.Models.Request;
using Dapper;

namespace CrudWeb.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly Infraestructure.DapperContext _context;

        public MotorcycleRepository(Infraestructure.DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddMotorcycleAsync(MotorcycleModel motorcycle)
        {
            var query = @"INSERT INTO motorcycles (identifier, year, model, license_plate) 
                          VALUES (@Identifier, @Year, @Model, @LicensePlate)";
            using (IDbConnection connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, motorcycle);
            }
        }

        public async Task<MotorcycleModel> GetByLicensePlateAsync(string licensePlate)
        {
            var query = "SELECT * FROM motorcycles WHERE license_plate = @LicensePlate LIMIT 1";
            using (IDbConnection connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<MotorcycleModel>(query, new { LicensePlate = licensePlate });
            }
        }

        public async Task<MotorcycleModel> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM motorcycles WHERE id = @Id LIMIT 1";
            using (IDbConnection connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<MotorcycleModel>(query, new { Id = id });
            }
        }

        public async Task UpdateLicensePlateAsync(int id, string licensePlate)
        {
            var query = "UPDATE motorcycles SET license_plate = @LicensePlate WHERE id = @Id";
            using (IDbConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { LicensePlate = licensePlate, Id = id });
            }
        }
        
        public async Task DeleteMotorcycleAsync(int id)
        {
            var query = "DELETE FROM motorcycles WHERE id = @Id";
            using (IDbConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

    }
}