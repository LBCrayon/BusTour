using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Place;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Utilities;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{

    public interface IPlaceServices
    {
        Task<BaseResponsePagingViewModel<PlaceResponse>> GetAllPlace(PagingRequest paging);
        Task<BaseResponseViewModel<PlaceResponse>> CreatePlace(CreatePlaceRequest request);
        Task<BaseResponseViewModel<PlaceResponse>> UpdatePlace(int placeId, UpdatePlaceRequest request);
    }
    public class PlaceServices : IPlaceServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlaceServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<BaseResponseViewModel<PlaceResponse>> CreatePlace(CreatePlaceRequest request)
        {
            var place = _mapper.Map<CreatePlaceRequest, Place>(request);

            await _unitOfWork.Repository<Place>().InsertAsync(place);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<PlaceResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<PlaceResponse>(place)
            };
        }

        public async Task<BaseResponsePagingViewModel<PlaceResponse>> GetAllPlace(PagingRequest paging)
        {
            try
            {

                {
                    var place = _unitOfWork.Repository<Place>().GetAll()
                                            .ProjectTo<PlaceResponse>(_mapper.ConfigurationProvider)
                                            .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                                             Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<PlaceResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = place.Item1
                        },
                        Data = place.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<PlaceResponse>> UpdatePlace(int placeId, UpdatePlaceRequest request)
        {
            var place = _unitOfWork.Repository<Place>().GetAll()
                .FirstOrDefault(x => x.Id == placeId);

            if (place == null)
                throw new ErrorResponse(404, (int)BusErrorEnums.NOT_FOUND,
                    BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updatePlace = _mapper.Map<UpdatePlaceRequest, Place>(request, place);


            await _unitOfWork.Repository<Place>().UpdateDetached(updatePlace);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<PlaceResponse>()
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

