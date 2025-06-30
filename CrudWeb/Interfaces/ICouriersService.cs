using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;

namespace CrudWeb.Interfaces
{
    public interface ICouriersService
    {
        Task<CouriersResponse> CreateCourierAsync(CouriersRequest request);
    }
}