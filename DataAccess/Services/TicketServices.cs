﻿using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Ticket;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Utilities;
using Microsoft.EntityFrameworkCore;
using static DataAccess.Helpers.ErrorEnum;

namespace DataAccess.Services
{
    public interface ITicketServices
    {
        Task<BaseResponsePagingViewModel<TicketResponse>> GetAllTicket(PagingRequest paging);
        Task<BaseResponseViewModel<TicketResponse>> CreateTicket(CreateTicketRequest request);
        Task<TicketResponse> GetTicketById(int ticketId);

        Task<BaseResponseViewModel<TicketResponse>> UpdateTicket(int ticketId, UpdateTicketRequest request);
        Task<BaseResponsePagingViewModel<TicketResponse>> GetTicketByTourId(int tourId, PagingRequest paging);
    }

    public class TicketServices : ITicketServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TicketServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseViewModel<TicketResponse>> CreateTicket(CreateTicketRequest request)
        {
            var tour = _unitOfWork.Repository<Tour>().GetAll().FirstOrDefault(x => x.Id == request.TourId);
            var clas = _unitOfWork.Repository<Class>().GetAll().FirstOrDefault(x => x.Id == request.ClassId);;
            if (tour == null || clas == null)
            {
                throw new ErrorResponse(404, 404, "tour or class not found");
            }

            var ticket = new Ticket();
            ticket.TotalPrice = request.UnitPrice + tour.TotalPrice + clas.Price;
            ticket.TotalTicket = (int)clas.Amount;
            ticket.UnitPrice = request.UnitPrice;
            ticket.Arrival = request.Arrival;
            ticket.Departure = request.Departure;
            ticket.StartTime = request.StartTime;
            ticket.EndTime = request.EndTime;
            ticket.AvailableTicket = ticket.TotalTicket;
            ticket.TourId = request.TourId;
            ticket.ClassId = request.ClassId;

            await _unitOfWork.Repository<Ticket>().InsertAsync(ticket);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<TicketResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<TicketResponse>(ticket)
            };
        }

        public Task<TicketResponse> GetTicketById(int ticketId)
        {
            try
            {
                {
                    var ticket = _unitOfWork.Repository<Ticket>().GetAll()
                        .Where(t => t.Id == ticketId).Include(t => t.Tour).Include(c => c.Class)
                        .ProjectTo<TicketResponse>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    if (ticket == null)
                    {
                        // Ticket with the specified ID not found
                        throw new ErrorResponse(404, 404, "Ticket not found.");
                    }

                    return ticket;
                }
            }
            catch (ErrorResponse ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponsePagingViewModel<TicketResponse>> GetAllTicket(PagingRequest paging)
        {
            try
            {
                {
                    var ticket = _unitOfWork.Repository<Ticket>().GetAll()
                        .ProjectTo<TicketResponse>(_mapper.ConfigurationProvider)
                        .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                            Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<TicketResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = ticket.Item1
                        },
                        Data = ticket.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<TicketResponse>> UpdateTicket(int ticketId, UpdateTicketRequest request)
        {
            var ticket = _unitOfWork.Repository<Ticket>().GetAll()
                .FirstOrDefault(x => x.Id == ticketId);

            if (ticket == null)
                throw new ErrorResponse(404, (int)BusErrorEnums.NOT_FOUND,
                    BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateTicket = _mapper.Map<UpdateTicketRequest, Ticket>(request, ticket);


            await _unitOfWork.Repository<Ticket>().UpdateDetached(updateTicket);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<TicketResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                }
            };
        }

        public async Task<BaseResponsePagingViewModel<TicketResponse>> GetTicketByTourId(int tourId, PagingRequest paging)
        {
            try
            {
                var ticket = _unitOfWork.Repository<Ticket>().GetAll()
                    .Where(x => x.TourId == tourId)
                    .ProjectTo<TicketResponse>(_mapper.ConfigurationProvider)
                    .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                        Constants.DefaultPaging);

                return new BaseResponsePagingViewModel<TicketResponse>()
                {
                    Metadata = new PagingsMetadata()
                    {
                        Page = paging.Page,
                        Size = paging.PageSize,
                        Total = ticket.Item1
                    },
                    Data = ticket.Item2.ToList()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}