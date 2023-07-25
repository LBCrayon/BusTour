using System;
using System.Data;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
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
        private readonly IVehicleServices _vehicleService;

        public BusController(IVehicleServices vehicleService)
        {
            _vehicleService = vehicleService;
        }


        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<VehicleResponse>>> GetAllVehicle
            ([FromQuery] VehicleResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _vehicleService.GetAllVehicle(paging);
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
        public async Task<ActionResult<BaseResponseViewModel<VehicleResponse>>> CreateVehicle
            ([FromBody] CreateVehicleRequest request)
        {
            try
            {
                return await _vehicleService.CreateVehicle(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{vehicleId}")]
        public async Task<ActionResult<BaseResponseViewModel<VehicleResponse>>> UpdateVehicle
            ([FromRoute] int vehicleId, [FromBody] UpdateVehicleRequest request)
        {
            try
            {
                return await _vehicleService.UpdateVehicle(vehicleId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}

