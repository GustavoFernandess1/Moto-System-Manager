using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Models;
using CrudWeb.Models.Request;

namespace CrudWeb.Mappers
{
    public class CouriersMapper
    {
        public CouriersRequest Map(CouriersRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O objeto CouriersRequest não pode ser nulo.");
            return new CouriersRequest
            {
                Identifier = request.Identifier,
                Name = request.Name,
                TaxId = request.TaxId,
                Birthdate = request.Birthdate,
                LicenseNumber = request.LicenseNumber,
                LicenseType = request.LicenseType,
                LicenseImage = request.LicenseImage ?? string.Empty
            };
        }

        public UploadLicenseImageRequest Map(UploadLicenseImageRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O objeto UploadCnhImageRequest não pode ser nulo.");
            return new UploadLicenseImageRequest
            {
                LicenseImage = request.LicenseImage ?? string.Empty
            };
        }
    }
}