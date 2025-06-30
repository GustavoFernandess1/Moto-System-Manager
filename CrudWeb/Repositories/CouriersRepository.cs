using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using CrudWeb.Models;
using System.Data;

namespace CrudWeb.Repositories
{
    public class CouriersRepository : ICouriersRepository
    {
        private readonly Infraestructure.DapperContext _context;

        public CouriersRepository(Infraestructure.DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CouriersModel>> GetAllAsync()
        {
            var query = "SELECT * FROM couriers";
            using (IDbConnection connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<CouriersModel>(query);
            }
        }

        public async Task<int> AddAsync(CouriersModel courier)
        {
            var query = @"INSERT INTO couriers (identifier, name, tax_id, birth_date, license_number, license_type, license_image) 
                          VALUES (@Identifier, @Name, @TaxId, @BirthDate, @LicenseNumber, @LicenseType, @LicenseImage)";
            using (IDbConnection connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, courier);
            }
        }
    }
}
