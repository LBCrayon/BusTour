using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Journey;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Utilities;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{

    public interface IJourneyServices
    {
        Task<BaseResponsePagingViewModel<JourneyResponse>> GetAllJourney(PagingRequest paging);
        Task<BaseResponseViewModel<JourneyResponse>> CreateJourney(CreateJourneyRequest request);
        Task<BaseResponseViewModel<JourneyResponse>> UpdateJourney(int journeyId, UpdateJourneyRequest request);
    }


    public class JourneyServices : IJourneyServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public JourneyServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<JourneyResponse>> CreateJourney(CreateJourneyRequest request)
        {
            var journey = _mapper.Map<CreateJourneyRequest, Journey>(request);

            await _unitOfWork.Repository<Journey>().InsertAsync(journey);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<JourneyResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<JourneyResponse>(journey)
            };
        }

        public async Task<BaseResponsePagingViewModel<JourneyResponse>> GetAllJourney(PagingRequest paging)
        {
            try
            {

                {
                    var journey = _unitOfWork.Repository<Journey>().GetAll()
                                            .ProjectTo<JourneyResponse>(_mapper.ConfigurationProvider)
                                            .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                                             Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<JourneyResponse>()
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

        public async Task<BaseResponseViewModel<JourneyResponse>> UpdateJourney(int journeyId, UpdateJourneyRequest request)
        {
            var journey = _unitOfWork.Repository<Journey>().GetAll()
                   .FirstOrDefault(x => x.Id == journeyId);

            if (journey == null)
                throw new ErrorResponse(404, (int)JourneyErrorEnums.NOT_FOUND,
                    JourneyErrorEnums.NOT_FOUND.GetDisplayName());

            var updateJourney = _mapper.Map<UpdateJourneyRequest, Journey>(request, journey);


            await _unitOfWork.Repository<Journey>().UpdateDetached(updateJourney);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<JourneyResponse>()
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

