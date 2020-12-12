using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HomeDevices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        public RegistrationController()
        {

        }

        //private readonly IServiceRegistration _serviceRegistration;

        //public RegistrationController(
        //    IServiceRegistration serviceRegistration
        //)
        //{
        //    _serviceRegistration = serviceRegistration;
        //}

        //[HttpPost("register/{serviceId}")]
        //public async Task<IActionResult> Register([FromRoute] string serviceId, [FromBody] ServiceRegistrationRequest serviceRegistrationRequest)
        //{
        //    try
        //    {
        //        if (serviceRegistrationRequest.ServiceId != serviceId)
        //        {
        //            throw new ApplicationException("The service id posted does not correspond to the service id specified in the URL.");
        //        }

        //        bool result = await _serviceRegistration.Register(serviceId, serviceRegistrationRequest.Host,
        //            serviceRegistrationRequest.Port, serviceRegistrationRequest.Scheme);

        //    }
        //    catch (Exception exc)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, exc);
        //    }

        //    return Ok();
        //}

        //[HttpPost("deregister/{serviceId}")]
        //public async Task<IActionResult> Deregister([FromRoute] string serviceId)
        //{
        //    try
        //    {
        //        bool result = await _serviceRegistration.Deregister(serviceId);

        //    }
        //    catch (Exception exc)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, exc);
        //    }

        //    return Ok();
        //}

        //[HttpGet("devices")]
        //public async Task<List<string>> GetDevices()
        //{
        //    var result = new List<string>();

        //    try
        //    {
        //        result = (await _serviceRegistration.GetServices()).ToList();
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
