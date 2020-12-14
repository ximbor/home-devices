using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using HomeDevices.Models;

namespace HomeDevices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        public RegistrationController()
        {

        }

        [HttpPost("device/register/{serviceId}")]
        public async Task<IActionResult> RegisterDevice([FromRoute] string DeviceId, [FromBody] DeviceRegistrationRequest deviceRegistrationRequest)
        {
            try
            {
                if (deviceRegistrationRequest.DeviceId != DeviceId)
                {
                    throw new ApplicationException("The device id posted does not correspond to the device id specified in the URL.");
                }

                throw new NotImplementedException("Devices registration is not available at the moment.");

            }
            catch (Exception exc)
            {
                return await Task.Run(() => StatusCode(StatusCodes.Status501NotImplemented, exc));
            }

            // return Ok();
        }

        [HttpPost("consumer/register/{serviceId}")]
        public async Task<IActionResult> RegisterConsumer([FromRoute] string ConsumerId, [FromBody] ConsumerRegistrationRequest consumerRegistrationRequest)
        {
            try
            {
                if (consumerRegistrationRequest.ConsumerId != ConsumerId)
                {
                    throw new ApplicationException("The consumer id posted does not correspond to the consumer id specified in the URL.");
                }

                throw new NotImplementedException("Consumers registration is not available at the moment.");

            }
            catch (Exception exc)
            {
                return await Task.Run(() => StatusCode(StatusCodes.Status501NotImplemented, exc));
            }

            // return Ok();
        }

    }
}
