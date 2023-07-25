using System;
using BusinessObject.Models;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Journey;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusTourApi.Controllers
{
        [Route(Helpers.SettingVersionApi.ApiVersion)]
        [ApiController]
        public class JourneyController : ControllerBase
        {
            private readonly IJourneyServices _journeyService;

            public JourneyController(IJourneyServices journeyService)
            {
                _journeyService = journeyService;
            }


        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<JourneyResponse>>> GetAllJourney
            ([FromQuery] VehicleResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _journeyService.GetAllJourney(paging);
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
        public async Task<ActionResult<BaseResponseViewModel<JourneyResponse>>> CreateJourney
            ([FromBody] CreateJourneyRequest request)
        {
            try
            {
                return await _journeyService.CreateJourney(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{journeyId}")]
        public async Task<ActionResult<BaseResponseViewModel<JourneyResponse>>> UpdateJourney
            ([FromRoute] int journeyId, [FromBody] UpdateJourneyRequest request)
        {
            try
            {
                return await _journeyService.UpdateJourney(journeyId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
    
}

