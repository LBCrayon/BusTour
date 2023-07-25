using System;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Place;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusTourApi.Controllers
{
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceServices _placeService;

        public PlaceController(IPlaceServices placeService)
        {
            _placeService = placeService;
        }


        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<PlaceResponse>>> GetAllPlace
            ([FromQuery] PlaceResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _placeService.GetAllPlace(paging);
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
        public async Task<ActionResult<BaseResponseViewModel<PlaceResponse>>> CreatePlace
            ([FromBody] CreatePlaceRequest request)
        {
            try
            {
                return await _placeService.CreatePlace(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{placeId}")]
        public async Task<ActionResult<BaseResponseViewModel<PlaceResponse>>> UpdatePlace
            ([FromRoute] int placeId, [FromBody] UpdatePlaceRequest request)
        {
            try
            {
                return await _placeService.UpdatePlace(placeId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

    }
}

