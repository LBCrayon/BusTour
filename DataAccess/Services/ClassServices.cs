using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Class;
using DataAccess.DTO.Request.Journey;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Utilities;
using Microsoft.EntityFrameworkCore;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{
    public interface IClassServices
    {
        Task<BaseResponsePagingViewModel<ClassResponse>> GetAllClass(PagingRequest paging);
        Task<BaseResponseViewModel<ClassResponse>> CreateClass(CreateClassRequest request);
        Task<BaseResponseViewModel<ClassResponse?>> GetClassById(int classId);

        Task<BaseResponseViewModel<ClassResponse>> UpdateClass(int classId, UpdateClassRequest request);

        Task<BaseResponseViewModel<ClassResponse>> DeleteClass(int classId);
    }

    public class ClassServices : IClassServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ClassServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<ClassResponse>> CreateClass(CreateClassRequest request)
        {
            var clas = _mapper.Map<CreateClassRequest, BusinessObject.Models.Class>(request);
            await _unitOfWork.Repository<BusinessObject.Models.Class>().InsertAsync(clas);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<ClassResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<ClassResponse>(clas)
            };
        }

        public async Task<BaseResponseViewModel<ClassResponse?>> GetClassById(int classId)
        {
            var clas = _unitOfWork.Repository<Class>().GetAll()
                .Where(t => t.Id == classId)
                .ProjectTo<ClassResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (clas == null)
            {
                throw new ErrorResponse(404, 404, "Class not found.");
            }

            return new BaseResponseViewModel<ClassResponse?>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                },
                Data = await clas
            };
        }


        public async Task<BaseResponsePagingViewModel<ClassResponse>> GetAllClass(PagingRequest paging)
        {
            try
            {
                {
                    var clas = _unitOfWork.Repository<Class>().GetAll()
                        .ProjectTo<ClassResponse>(_mapper.ConfigurationProvider)
                        .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                            Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<ClassResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = clas.Item1
                        },
                        Data = clas.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponseViewModel<ClassResponse>> UpdateClass(int classId, UpdateClassRequest request)
        {
            var clas = _unitOfWork.Repository<Class>().GetAll()
                .FirstOrDefault(x => x.Id == classId);

            if (clas == null)
                throw new ErrorResponse(404, (int)ClassErrorEnums.NOT_FOUND,
                    ClassErrorEnums.NOT_FOUND.GetDisplayName());

            var updateClass = _mapper.Map<UpdateClassRequest, Class>(request, clas);


            await _unitOfWork.Repository<Class>().UpdateDetached(updateClass);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<ClassResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                }
            };
        }

        public async Task<BaseResponseViewModel<ClassResponse>> DeleteClass(int classId)
        {
            var clas = _unitOfWork.Repository<Class>().GetById(classId).Result;

            if (clas == null)
                throw new ErrorResponse(404, (int)ClassErrorEnums.NOT_FOUND,
                    ClassErrorEnums.NOT_FOUND.GetDisplayName());

            _unitOfWork.Repository<Class>().Delete(clas);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<ClassResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                }
            };
        }
    }
}