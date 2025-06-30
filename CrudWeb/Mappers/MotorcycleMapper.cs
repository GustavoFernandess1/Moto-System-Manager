using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Models.Request;

namespace CrudWeb.Mappers
{
    public class MotorcycleMapper
    {
        public MotorcycleRegisterRequest Map(MotorcycleRegisterRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O objeto MotorcycleRegisterRequest não pode ser nulo.");
            return new MotorcycleRegisterRequest
            {
                Identifier = request.Identifier,
                Year = request.Year,
                Model = request.Model,
                LicensePlate = request.LicensePlate
            };
        }

        public UpdateLicensePlateRequest Map(UpdateLicensePlateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O objeto UpdateLicensePlateRequest não pode ser nulo.");
            return new UpdateLicensePlateRequest
            {
                LicensePlate = request.LicensePlate,
            };
        }
    }
}