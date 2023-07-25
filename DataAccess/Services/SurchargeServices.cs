using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Surcharge;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Utilities;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{

    public interface ISurchargeServices
    {
        Task<BaseResponsePagingViewModel<SurchargeResponse>> GetAllSurcharge(PagingRequest paging);
        Task<BaseResponseViewModel<SurchargeResponse>> CreateSurcharge(CreateSurchargeRequest request);
        Task<BaseResponseViewModel<SurchargeResponse>> UpdateSurcharge(int surchargeId, UpdateSurchargeRequest request);
    }

    public class SurchargeServices : ISurchargeServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SurchargeServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<SurchargeResponse>> CreateSurcharge(CreateSurchargeRequest request)
        {
            var surcharge = _mapper.Map<CreateSurchargeRequest, Surcharge>(request);

            await _unitOfWork.Repository<Surcharge>().InsertAsync(surcharge);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<SurchargeResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<SurchargeResponse>(surcharge)
            }; throw new NotImplementedException();
        }

        public async Task<BaseResponsePagingViewModel<SurchargeResponse>> GetAllSurcharge(PagingRequest paging)
        {
            try
            {

                {
                    var surcharge = _unitOfWork.Repository<Surcharge>().GetAll()
                                            .ProjectTo<SurchargeResponse>(_mapper.ConfigurationProvider)
                                            .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                                             Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<SurchargeResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = surcharge.Item1
                        },
                        Data = surcharge.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<SurchargeResponse>> UpdateSurcharge(int surchargeId, UpdateSurchargeRequest request)
        {
            var surcharge = _unitOfWork.Repository<Surcharge>().GetAll()
                 .FirstOrDefault(x => x.Id == surchargeId);

            if (surcharge == null)
                throw new ErrorResponse(404, (int)BusErrorEnums.NOT_FOUND,
                    BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateSurcharge = _mapper.Map<UpdateSurchargeRequest, Surcharge>(request, surcharge);


            await _unitOfWork.Repository<Surcharge>().UpdateDetached(updateSurcharge);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<SurchargeResponse>()
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

