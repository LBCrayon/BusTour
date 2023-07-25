using AutoMapper;
using BusinessObject.Models;
using Castle.Core.Resource;
using DataAccess.DTO.Request.Vehicle;
using DataAccess.DTO.Request.Journey;
using DataAccess.DTO.Request.Class;
using DataAccess.DTO.Request.Medium;
using DataAccess.DTO.Request.Place;
using DataAccess.DTO.Request.Surcharge;
using DataAccess.DTO.Request.Ticket;
using DataAccess.DTO.Request.Tour;
using DataAccess.DTO.Request.TourPlace;
using DataAccess.DTO.Response;

namespace BusTourApi.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()

        {
            #region Bus
            CreateMap<Vehicle, VehicleResponse>().ReverseMap();
            CreateMap<CreateVehicleRequest, Vehicle>();
            CreateMap<UpdateVehicleRequest, Vehicle>();
            #endregion
            #region Journey
            CreateMap<Journey, ClassResponse>().ReverseMap();
            CreateMap<CreateJourneyRequest, Journey>();
            CreateMap<UpdateJourneyRequest, Journey>();
            #endregion
            #region Medium
            CreateMap<Medium, MediumResponse>().ReverseMap();
            CreateMap<CreateMediumRequest, Medium>();
            CreateMap<UpdateMediumRequest, Medium>();
            #endregion
            #region Place
            CreateMap<Place, PlaceResponse>().ReverseMap();
            CreateMap<CreatePlaceRequest, Place>();
            CreateMap<UpdatePlaceRequest, Place>();
            #endregion
            #region Surcharge
            CreateMap<Surcharge, SurchargeResponse>().ReverseMap();
            CreateMap<CreateSurchargeRequest, Surcharge>();
            CreateMap<UpdateSurchargeRequest, Surcharge>();
            #endregion
            #region Ticket
            CreateMap<Ticket, TicketResponse>().ReverseMap();
            CreateMap<CreateTicketRequest, Ticket>();
            CreateMap<UpdateTicketRequest, Ticket>();
            #endregion
            #region Tour
            CreateMap<Tour, TourResponse>().ReverseMap();
            CreateMap<CreateTourRequest, Tour>();
            CreateMap<UpdateTourRequest, Tour>();
            #endregion
            #region TourPlace
            CreateMap<TourPlace, TourPlaceResponse>().ReverseMap();
            CreateMap<CreateTourPlaceRequest, TourPlace>();
            CreateMap<UpdateTourPlaceRequest, TourPlace>();
            #endregion
            #region Class
            CreateMap<Class, ClassResponse>().ReverseMap();
            CreateMap<CreateClassRequest, Class>();
            CreateMap<UpdateClassRequest, TourPlace>();
            #endregion
        }
    }
}
