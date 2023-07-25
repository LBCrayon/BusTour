using System;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Tour;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusTourApi.Controllers
{
   
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourServices _tourService;

        public TourController(ITourServices tourService)
        {
            _tourService = tourService;
        }


        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<TourResponse>>> GetAllTour
            ([FromQuery] TourResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _tourService.GetAllTour(paging);
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
        public async Task<ActionResult<BaseResponseViewModel<TourResponse>>> CreateTour
            ([FromBody] CreateTourRequest request)
        {
            try
            {
                return await _tourService.CreateTour(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{tourId}")]
        public async Task<ActionResult<BaseResponseViewModel<TourResponse>>> UpdateStore
            ([FromRoute] int tourId, [FromBody] UpdateTourRequest request)
        {
            try
            {
                return await _tourService.UpdateTour(tourId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}

