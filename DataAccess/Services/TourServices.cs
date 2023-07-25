using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Tour;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Utilities;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{
    public interface ITourServices
    {
        Task<BaseResponsePagingViewModel<TourResponse>> GetAllTour(PagingRequest paging);
        Task<BaseResponseViewModel<TourResponse>> CreateTour(CreateTourRequest request);
        Task<BaseResponseViewModel<TourResponse>> UpdateTour(int tourId, UpdateTourRequest request);
    }
    public class TourServices : ITourServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TourServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<TourResponse>> CreateTour(CreateTourRequest request)
        {
            var tour = _mapper.Map<CreateTourRequest, Tour>(request);

            await _unitOfWork.Repository<Tour>().InsertAsync(tour);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<TourResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<TourResponse>(tour)
            };
        }

        public async Task<BaseResponsePagingViewModel<TourResponse>> GetAllTour(PagingRequest paging)
        {
            try
            {

                {
                    var tour = _unitOfWork.Repository<Tour>().GetAll()
                                            .ProjectTo<TourResponse>(_mapper.ConfigurationProvider)
                                            .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                                             Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<TourResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = tour.Item1
                        },
                        Data = tour.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<TourResponse>> UpdateTour(int tourId, UpdateTourRequest request)
        {
            var tour = _unitOfWork.Repository<Tour>().GetAll()
                 .FirstOrDefault(x => x.Id == tourId);

            if (tour == null)
                throw new ErrorResponse(404, (int)BusErrorEnums.NOT_FOUND,
                    BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateTour = _mapper.Map<UpdateTourRequest, Tour>(request, tour);


            await _unitOfWork.Repository<Tour>().UpdateDetached(updateTour);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<TourResponse>()
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

