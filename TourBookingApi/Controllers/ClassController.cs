using System;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Class;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusTourApi.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController:ControllerBase
	{
        private readonly IClassServices _classService;

        public ClassController(IClassServices classService)
        {
            _classService = classService;
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<ClassResponse>>> GetAllClass
            ([FromQuery] ClassResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _classService.GetAllClass(paging);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
        
        [HttpGet("{classId}")]
        public async Task<ActionResult<BaseResponseViewModel<ClassResponse>>> GetClassById
            ([FromRoute] int classId)
        {
            try
            {
                return await _classService.GetClassById(classId);
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
        public async Task<ActionResult<BaseResponseViewModel<ClassResponse>>> CreateClass
            ([FromBody] CreateClassRequest request)
        {
            try
            {
                return await _classService.CreateClass(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{classId}")]
        public async Task<ActionResult<BaseResponseViewModel<ClassResponse>>> UpdateClass
            ([FromRoute] int classId, [FromBody] UpdateClassRequest request)
        {
            try
            {
                return await _classService.UpdateClass(classId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
        
        [HttpDelete("{classId}")]
        public async Task<ActionResult<BaseResponseViewModel<ClassResponse>>> DeleteClass
            ([FromRoute] int classId)
        {
            try
            {
                return await _classService.DeleteClass(classId);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}

