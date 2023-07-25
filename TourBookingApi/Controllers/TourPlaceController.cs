using System;
using BusinessObject.Models;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.TourPlace;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusTourApi.Controllers
{
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class TourPlaceController : ControllerBase
    {
        private readonly ITourPlaceServices _tourplaceService;

        public TourPlaceController(ITourPlaceServices tourplaceService)
        {
            _tourplaceService = tourplaceService;
        }


        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<TourPlaceResponse>>> GetAllTourPlace
            ([FromQuery] TourPlaceResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _tourplaceService.GetAllTourPlace(paging);
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
        public async Task<ActionResult<BaseResponseViewModel<TourPlaceResponse>>> CreateTourPlace
            ([FromBody] CreateTourPlaceRequest request)
        {
            try
            {
                return await _tourplaceService.CreateTourPlace(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{tourplaceId}")]
        public async Task<ActionResult<BaseResponseViewModel<TourPlaceResponse>>> UpdateTourPlace
            ([FromRoute] int tourplaceId, [FromBody] UpdateTourPlaceRequest request)
        {
            try
            {
                return await _tourplaceService.UpdateTourPlace(tourplaceId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}

