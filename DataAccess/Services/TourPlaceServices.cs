using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.TourPlace;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Utilities;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{
    public interface ITourPlaceServices
    {
        Task<BaseResponsePagingViewModel<TourPlaceResponse>> GetAllTourPlace(PagingRequest paging);
        Task<BaseResponseViewModel<TourPlaceResponse>> CreateTourPlace(CreateTourPlaceRequest request);
        Task<BaseResponseViewModel<TourPlaceResponse>> UpdateTourPlace(int tourPlaceId, UpdateTourPlaceRequest request);
    }


    public class TourPlaceServices : ITourPlaceServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TourPlaceServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<TourPlaceResponse>> CreateTourPlace(CreateTourPlaceRequest request)
        {
            var tourplace = _mapper.Map<CreateTourPlaceRequest, TourPlace>(request);

            await _unitOfWork.Repository<TourPlace>().InsertAsync(tourplace);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<TourPlaceResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<TourPlaceResponse>(tourplace)
            };
        }

        public async Task<BaseResponsePagingViewModel<TourPlaceResponse>> GetAllTourPlace(PagingRequest paging)
        {
            try
            {

                {
                    var tourplace = _unitOfWork.Repository<TourPlace>().GetAll()
                                            .ProjectTo<TourPlaceResponse>(_mapper.ConfigurationProvider)
                                            .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                                             Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<TourPlaceResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = tourplace.Item1
                        },
                        Data = tourplace.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<TourPlaceResponse>> UpdateTourPlace(int tourPlaceId, UpdateTourPlaceRequest request)
        {
            var tourplace = _unitOfWork.Repository<TourPlace>().GetAll()
                  .FirstOrDefault(x => x.Id == tourPlaceId);

            if (tourplace == null)
                throw new ErrorResponse(404, (int)BusErrorEnums.NOT_FOUND,
                    BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateTourplace = _mapper.Map<UpdateTourPlaceRequest, TourPlace>(request, tourplace);


            await _unitOfWork.Repository<TourPlace>().UpdateDetached(updateTourplace);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<TourPlaceResponse>()
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

