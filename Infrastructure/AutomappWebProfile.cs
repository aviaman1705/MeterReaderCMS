using AutoMapper;
using MeterReaderCMS.Models.DTO;
using MeterReaderCMS.Models.DTO.Track;
using MeterReaderCMS.Models.DTO.User;
using MeterReaderCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeterReaderCMS.Infrastructure
{
    public class AutomappWebProfile : Profile
    {
        public AutomappWebProfile()
        {
            #region User

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, UserProfileDTO>()
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId)).ReverseMap();
            #endregion


            #region Track
            CreateMap<CreateTrackDTO, Track>()
               .ForMember(dest => dest.ElectricityMeterCalled, opt => opt.MapFrom(src => src.Called))
               .ForMember(dest => dest.ElectricityMeterUnCalled, opt => opt.MapFrom(src => src.UnCalled))
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.ParseExact(src.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
            
            CreateMap<Track, EditTrackDTO>()
             .ForMember(dest => dest.Called, opt => opt.MapFrom(src => src.ElectricityMeterCalled))
             .ForMember(dest => dest.UnCalled, opt => opt.MapFrom(src => src.ElectricityMeterUnCalled))
             .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))).ReverseMap();

            CreateMap<Track, TrackListItemDTO>()
                .ForMember(dest => dest.NoteBookNumber, opt => opt.MapFrom(src => src.NoteBook.Number))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.NoteBook.StreetName))
                .ForMember(dest => dest.Called, opt => opt.MapFrom(src => src.ElectricityMeterCalled))
                .ForMember(dest => dest.UnCalled, opt => opt.MapFrom(src => src.ElectricityMeterUnCalled));
            #endregion

        }

        public static void Run()
        {
            Mapper.Initialize(a => { a.AddProfile<AutomappWebProfile>(); });
        }
    }
}