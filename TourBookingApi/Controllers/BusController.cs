using System;
using System.Data;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Bus;
using DataAccess.DTO.Response;
using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTQ.Sdk.Core.Filters;

namespace BusTourApi.Controllers
{
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusServices _busService;

        public BusController(IBusServices busService)
        {
            _busService = busService;
        }


        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<BusResponse>>> GetAllBus
            ([FromQuery] BusResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _busService.GetAllBus(paging);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Create Bus
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BaseResponseViewModel<BusResponse>>> CreateBus
            ([FromBody] CreateBusRequest request)
        {
            try
            {
                return await _busService.CreateBus(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{busId}")]
        public async Task<ActionResult<BaseResponseViewModel<BusResponse>>> UpdateStore
            ([FromRoute] int busId, [FromBody] UpdateBusRequest request)
        {
            try
            {
                return await _busService.UpdateBus(busId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}

