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
using DataAccess.DTO.Request.Vehicle;
using static System.Formats.Asn1.AsnWriter;
using DataAccess.Exceptions;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{
    public interface IVehicleServices
    {
        Task<BaseResponsePagingViewModel<VehicleResponse>> GetAllVehicle(PagingRequest paging);
        Task<BaseResponseViewModel<VehicleResponse>> CreateVehicle(CreateVehicleRequest request);
        Task<BaseResponseViewModel<VehicleResponse>> UpdateVehicle(int busId, UpdateVehicleRequest request);
    }


    public class VehicleServices : IVehicleServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<VehicleResponse>> CreateVehicle(CreateVehicleRequest request)
        {
            var bus = _mapper.Map<CreateVehicleRequest, Vehicle>(request);

            await _unitOfWork.Repository<Vehicle>().InsertAsync(bus);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<VehicleResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<VehicleResponse>(bus)
            };
        }


        public async Task<BaseResponsePagingViewModel<VehicleResponse>> GetAllVehicle(PagingRequest paging)
        {
            try
            {

                {
                    var bus = _unitOfWork.Repository<Vehicle>().GetAll()
                                            .ProjectTo<VehicleResponse>(_mapper.ConfigurationProvider)
                                            .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                                             Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<VehicleResponse>()
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

        public async Task<BaseResponseViewModel<VehicleResponse>> UpdateVehicle(int busId, UpdateVehicleRequest request)
        {
            var bus = _unitOfWork.Repository<Vehicle>().GetAll()
                 .FirstOrDefault(x => x.Id == busId);

            if (bus == null)
                throw new ErrorResponse(404, (int)BusErrorEnums.NOT_FOUND,
                    BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateBus = _mapper.Map<UpdateVehicleRequest, Vehicle>(request, bus);


            await _unitOfWork.Repository<Vehicle>().UpdateDetached(updateBus);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<VehicleResponse>()
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

