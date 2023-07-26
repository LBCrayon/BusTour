﻿using System;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Medium;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusTourApi.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediumServices _mediumService;

        public MediaController(IMediumServices mediumService)
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
        [HttpPut("{mediaId}")]
        public async Task<ActionResult<BaseResponseViewModel<MediumResponse>>> UpdateMedium
            ([FromRoute] int mediaId, [FromBody] UpdateMediumRequest request)
        {
            try
            {
                return await _mediumService.UpdateMedium(mediaId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        [HttpDelete("{mediaId}")]
        public async Task<ActionResult<BaseResponseViewModel<MediumResponse>>> DeleteMedium
            ([FromRoute] int mediumId)
        {
            try
            {
                return await _mediumService.DeleteMedia(mediumId);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}