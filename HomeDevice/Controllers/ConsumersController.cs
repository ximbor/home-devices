using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDevices.Core.Database.Exceptions;
using HomeDevices.Core.Database.Providers;
using HomeDevices.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeDevices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumersController : ControllerBase
    {
        private readonly IDataProvider _dataProvider;

        public ConsumersController(IDataProvider DataProvider)
        {
            _dataProvider = DataProvider;
        }

        [HttpGet()]
        public async Task<IActionResult> GetConsumers()
        {
            try
            {
                var result = await _dataProvider.ConsumersGet();
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

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetConsumer([FromQuery] string Id)
        {
            try
            {
                var guid = Guid.Parse(Id);
                var result = await _dataProvider.ConsumerGet(guid);
                return Ok(result);
            }
            catch (FormatException)
            {
                return BadRequest("Please specify a valid guid for the consumer.");
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
        public async Task<IActionResult> AddConsumer([FromBody] ConsumerRequestAdd Request)
        {
            try
            {
                var result = await _dataProvider.ConsumerAdd(Request.ToConsumer());
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
        public async Task<IActionResult> UpdateConsumer([FromQuery] string Id, [FromBody] ConsumerRequestUpdate Request)
        {
            try
            {
                var guid = Guid.Parse(Id);
                if(guid.ToString() != Id)
                {
                    return BadRequest("The consumer Id specified in the body is different from the one specified in the URL.");
                }
                var result = await _dataProvider.ConsumerUpdate(Request.ToConsumer());
                return Ok(result);
            }
            catch (FormatException)
            {
                return BadRequest("Please specify a valid guid for the consumer.");
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
        public async Task<IActionResult> RemoveConsumer([FromQuery] string Id)
        {
            try
            {
                var guid = Guid.Parse(Id);
                var result = await _dataProvider.ConsumerDelete(guid);
                return Ok(result);
            }
            catch(FormatException)
            {
                return BadRequest("Please specify a valid guid for the consumer.");
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
