using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.DTO.Request;
using DataAccess.DTO.Response;
using DataAccess.Utilities;
using DataAccess.Attributes;
using System.Threading.Tasks;
using DataAccess.DTO.Request.Bus;
using static System.Formats.Asn1.AsnWriter;
using DataAccess.Exceptions;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{
    public interface IBusServices
	{
        Task<BaseResponsePagingViewModel<BusResponse>> GetAllBus(PagingRequest paging);
        Task<BaseResponseViewModel<BusResponse>> CreateBus(CreateBusRequest request);
        Task<BaseResponseViewModel<BusResponse>> UpdateBus(int busId, UpdateBusRequest request);
    }


    public class BusServices : IBusServices
	{
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BusServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<BusResponse>> CreateBus(CreateBusRequest request)
        {
            var bus = _mapper.Map<CreateBusRequest, Bus>(request);

            await _unitOfWork.Repository<Bus>().InsertAsync(bus);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<BusResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<BusResponse>(bus)
            };
        }

      
        public async Task<BaseResponsePagingViewModel<BusResponse>> GetAllBus(PagingRequest paging)
        {
            try
            {

                {
                    var bus = _unitOfWork.Repository<Bus>().GetAll()
                                            .ProjectTo<BusResponse>(_mapper.ConfigurationProvider)
                                            .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                                             Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<BusResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = bus.Item1
                        },
                        Data = bus.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<BusResponse>> UpdateBus(int busId, UpdateBusRequest request)
        {
            var bus = _unitOfWork.Repository<Bus>().GetAll()
                 .FirstOrDefault(x => x.Id == busId);

            if (bus == null)
                throw new ErrorResponse(404, (int)BusErrorEnums.NOT_FOUND,
                    BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateBus = _mapper.Map<UpdateBusRequest, Bus>(request, bus);


            await _unitOfWork.Repository<Bus>().UpdateDetached(updateBus);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<BusResponse>()
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

