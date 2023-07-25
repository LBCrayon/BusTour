using System;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Surcharge;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusTourApi.Controllers
{
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class SurchargeController : ControllerBase
    {
        private readonly ISurchargeServices _surchargeService;

        public SurchargeController(ISurchargeServices surchargeService)
        {
            _surchargeService = surchargeService;
        }


        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<SurchargeResponse>>> GetAllSurcharge
            ([FromQuery] SurchargeResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _surchargeService.GetAllSurcharge(paging);
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
        public async Task<ActionResult<BaseResponseViewModel<SurchargeResponse>>> CreateSurcharge
            ([FromBody] CreateSurchargeRequest request)
        {
            try
            {
                return await _surchargeService.CreateSurcharge(request);
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
        public async Task<ActionResult<BaseResponseViewModel<SurchargeResponse>>> UpdateSurcharge
            ([FromRoute] int surchargeId, [FromBody] UpdateSurchargeRequest request)
        {
            try
            {
                return await _surchargeService.UpdateSurcharge(surchargeId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}

