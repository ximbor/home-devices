using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDevices.Core.Database.Exceptions;
using HomeDevices.Core.Database.Providers;
using HomeDevices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeDevices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDataProvider _dataProvider;

        public DevicesController(IDataProvider DataProvider)
        {
            _dataProvider = DataProvider;
        }

        [HttpGet()]
        public async Task<IActionResult> GetDevices()
        {
            try
            {
                var result = await _dataProvider.DevicesGet();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDevice([FromQuery] string Id)
        {
            try
            {
                var guid = Guid.Parse(Id);
                var result = await _dataProvider.DeviceGet(guid);
                return Ok(result);
            }
            catch (FormatException)
            {
                return BadRequest("Please specify a valid guid for the device.");
            }
            catch (EntityNotFoundException exc)
            {
                return NotFound(exc.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddDevice([FromBody] DeviceRequestAdd Request)
        {
            try
            {
                var result = await _dataProvider.DeviceAdd(Request.ToDevice());
                return Ok(result);
            }
            catch (EntityNotFoundException exc)
            {
                return NotFound(exc.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateDevice([FromQuery] string Id, [FromBody] DeviceRequestUpdate Request)
        {
            try
            {
                var guid = Guid.Parse(Id);
                if (guid.ToString() != Id)
                {
                    return BadRequest("The device Id specified in the body is different from the one specified in the URL.");
                }
                var result = await _dataProvider.DeviceUpdate(Request.ToDevice());
                return Ok(result);
            }
            catch (FormatException)
            {
                return BadRequest("Please specify a valid guid for the device.");
            }
            catch (EntityNotFoundException exc)
            {
                return NotFound(exc.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> RemoveDevice([FromQuery] string Id)
        {
            try
            {
                var guid = Guid.Parse(Id);
                var result = await _dataProvider.DeviceDelete(guid);
                return Ok(result);
            }
            catch (FormatException)
            {
                return BadRequest("Please specify a valid guid for the device.");
            }
            catch (EntityNotFoundException exc)
            {
                return NotFound(exc.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
