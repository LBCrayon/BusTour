using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Journey;
using DataAccess.DTO.Request.Medium;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Utilities;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{

    public interface IMediumServices
    {
        Task<BaseResponsePagingViewModel<MediumResponse>> GetAllMedium(PagingRequest paging);
        Task<BaseResponseViewModel<MediumResponse>> CreateMedium(CreateMediumRequest request);
        Task<BaseResponseViewModel<MediumResponse>> UpdateMedium(int mediumId, UpdateMediumRequest request);
    }
    public class MediumServices : IMediumServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MediumServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<MediumResponse>> CreateMedium(CreateMediumRequest request)
        {
            var medium = _mapper.Map<CreateMediumRequest, Medium>(request);

            await _unitOfWork.Repository<Medium>().InsertAsync(medium);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<MediumResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<MediumResponse>(medium)
            };
        }

        public async Task<BaseResponsePagingViewModel<MediumResponse>> GetAllMedium(PagingRequest paging)
        {
            try
            {

                {
                    var journey = _unitOfWork.Repository<Medium>().GetAll()
                                            .ProjectTo<MediumResponse>(_mapper.ConfigurationProvider)
                                            .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                                             Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<MediumResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = journey.Item1
                        },
                        Data = journey.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<MediumResponse>> UpdateMedium(int mediumId, UpdateMediumRequest request)
        {
            var medium = _unitOfWork.Repository<Medium>().GetAll()
         .FirstOrDefault(x => x.Id == mediumId);

            if (medium == null)
                throw new ErrorResponse(404, (int)JourneyErrorEnums.NOT_FOUND,
                    JourneyErrorEnums.NOT_FOUND.GetDisplayName());

            var updateMedium = _mapper.Map<UpdateMediumRequest, Medium>(request, medium);


            await _unitOfWork.Repository<Medium>().UpdateDetached(updateMedium);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<MediumResponse>()
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

