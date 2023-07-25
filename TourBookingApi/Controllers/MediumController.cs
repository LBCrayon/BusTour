using System;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Medium;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusTourApi.Controllers
{
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class MediumController : ControllerBase
    {
        private readonly IMediumServices _mediumService;

        public MediumController(IMediumServices mediumService)
        {
            _mediumService = mediumService;
        }
        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<MediumResponse>>> GetAllBus
            ([FromQuery] MediumResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _mediumService.GetAllMedium(paging);
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
        public async Task<ActionResult<BaseResponseViewModel<MediumResponse>>> CreateMedium
            ([FromBody] CreateMediumRequest request)
        {
            try
            {
                return await _mediumService.CreateMedium(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{mediumId}")]
        public async Task<ActionResult<BaseResponseViewModel<MediumResponse>>> UpdateMedium
            ([FromRoute] int mediumId, [FromBody] UpdateMediumRequest request)
        {
            try
            {
                return await _mediumService.UpdateMedium(mediumId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

    }
}

