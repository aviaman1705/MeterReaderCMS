using AutoMapper;
using MeterReaderCMS.Areas.admin.Data;
using MeterReaderCMS.Models.DTO;
using MeterReaderCMS.Models.DTO.Track;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Models.ViewModels;
using MeterReaderCMS.Models.ViewModels.MeterReader;
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

            CreateMap<CreateTrackDTO, Track>()
                .ForMember(dest => dest.ElectricityMeterCalled, opt => opt.MapFrom(src => src.Called))
                .ForMember(dest => dest.ElectricityMeterUnCalled, opt => opt.MapFrom(src => src.UnCalled));

            CreateMap<Track, TrackListItemDTO>()
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.NoteBook.StreetName))
                .ForMember(dest => dest.Called, opt => opt.MapFrom(src => src.ElectricityMeterCalled))
                .ForMember(dest => dest.UnCalled, opt => opt.MapFrom(src => src.ElectricityMeterUnCalled));

            CreateMap<Track, SelectListItem>()
                  .ForMember(dest => dest.Disabled, opt => opt.MapFrom(src => true))
                  .ForMember(dest => dest.Selected, opt => opt.MapFrom(src => true))
                  .ForMember(dest => dest.Text, 
                  opt => opt.MapFrom(src => $"{src.ElectricityMeterCalled} {src.ElectricityMeterUnCalled} {DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}"));
                

            CreateMap<MeterReader, AddBoulderVM>().ReverseMap();

            CreateMap<MeterReader, EditBoulderVM>()
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<EditBoulderVM, MeterReader>()
              .ForMember(dest => dest.Called, opt => opt.MapFrom(src => src.Called))
              .ForMember(dest => dest.Left, opt => opt.MapFrom(src => src.Left))
              .ForMember(dest => dest.UnCalled, opt => opt.MapFrom(src => src.UnCalled));

            CreateMap<MeterReader, MeterReaderGridVM>()
              .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => (src.NoteBook.StreetName)))
              .ForMember(dest => dest.NoteBookNumber, opt => opt.MapFrom(src => src.NoteBook.Number));

            CreateMap<UserVM, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

            CreateMap<User, UserProfileVM>()
                .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

            CreateMap<UserProfileVM, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        }
        public static void Run()
        {
            Mapper.Initialize(a => { a.AddProfile<AutomappWebProfile>(); });
        }
    }
}