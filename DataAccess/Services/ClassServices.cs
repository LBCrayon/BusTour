  using AutoMapper;
  using AutoMapper.QueryableExtensions;
  using BusinessObject.Models;
  using BusinessObject.UnitOfWork;
  using DataAccess.Attributes;
  using DataAccess.DTO.Request;
  using DataAccess.DTO.Request.Class;
  using DataAccess.DTO.Response;
  using DataAccess.Exceptions;
  using DataAccess.Helpers;
  using DataAccess.Utilities;

  namespace DataAccess.Services
{
    public interface IClassServices
    {
        Task<BaseResponsePagingViewModel<ClassResponse>> GetAllClass(PagingRequest paging);
        Task<BaseResponseViewModel<ClassResponse>> CreateClass(CreateClassRequest request);
        Task<BaseResponseViewModel<ClassResponse>> UpdateClass(int classId, UpdateClassRequest request);
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

        public async Task<BaseResponsePagingViewModel<ClassResponse>> GetAllClass(PagingRequest paging)
        {
            try
            {

                {
                    var bus = _unitOfWork.Repository<Class>().GetAll()
                        .ProjectTo<ClassResponse>(_mapper.ConfigurationProvider)
                        .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                            Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<ClassResponse>()
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

        public async Task<BaseResponseViewModel<ClassResponse>> CreateClass(CreateClassRequest request)
        {
            var bus = _mapper.Map<CreateClassRequest, Class>(request);

            await _unitOfWork.Repository<Class>().InsertAsync(bus);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<ClassResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<ClassResponse>(bus)
            };
        }

        public async Task<BaseResponseViewModel<ClassResponse>> UpdateClass(int classId, UpdateClassRequest request)
        {
            var bus = _unitOfWork.Repository<Class>().GetAll()
                .FirstOrDefault(x => x.Id == classId);

            if (bus == null)
                throw new ErrorResponse(404, (int)ErrorEnum.BusErrorEnums.NOT_FOUND,
                    ErrorEnum.BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateBus = _mapper.Map<UpdateClassRequest, Class>(request, bus);


            await _unitOfWork.Repository<Class>().UpdateDetached(updateBus);
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
