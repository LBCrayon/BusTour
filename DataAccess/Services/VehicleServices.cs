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
using Microsoft.EntityFrameworkCore;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{
    public interface IVehicleServices
    {
        Task<BaseResponsePagingViewModel<VehicleResponse>> GetAllVehicle(PagingRequest paging);
        Task<BaseResponseViewModel<VehicleResponse?>> GetVehicleById(int vehicleId);
        Task<BaseResponseViewModel<VehicleResponse>> CreateVehicle(CreateVehicleRequest request);
        Task<BaseResponseViewModel<VehicleResponse>> UpdateVehicle(int busId, UpdateVehicleRequest request);

        Task<BaseResponseViewModel<VehicleResponse>> DeleteVehicle(int busId);
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

        public async Task<BaseResponseViewModel<VehicleResponse?>> GetVehicleById(int vehicleId)
        {
            try
            {
                {
                    var vehicle = _unitOfWork.Repository<Vehicle>().GetAll()
                        .Where(t => t.Id == vehicleId)
                        .ProjectTo<VehicleResponse>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    if (vehicle == null)
                    {
                        // Ticket with the specified ID not found
                        throw new ErrorResponse(404, 404, "Bus not found.");
                    }


                    return new BaseResponseViewModel<VehicleResponse?>()
                    {
                        Status = new StatusViewModel()
                        {
                            Message = "Success",
                            Success = true,
                            
                        },
                        Data = await vehicle
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<BaseResponseViewModel<VehicleResponse>> DeleteVehicle(int busId)
        {
            var bus = _unitOfWork.Repository<Vehicle>().GetById(busId).Result;

            if (bus == null)
                throw new ErrorResponse(404, (int)BusErrorEnums.NOT_FOUND,
                    BusErrorEnums.NOT_FOUND.GetDisplayName());


            _unitOfWork.Repository<Vehicle>().Delete(bus);
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