using AutoMapper;
using GymManagmentBLL.ViewModels.MemberSessionViewModel;
using GymManagmentBLL.ViewModels.MemberViewMode_s;
using GymManagmentBLL.ViewModels.PlanViewModels;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using GymManagmentDAL.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapTrainer();
            MapSession();

            MapMember();
            MapPlan();
            MapBooking();

        }
        private void MapTrainer()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()
                   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                   {
                       BuldingNo = src.BuildingNumber,
                       Street = src.Street,
                       City = src.City
                   }));
            CreateMap<Trainer, TrainerViewModel>()
                            .ForMember(dest => dest.Address,
                            opt => opt.MapFrom(src => $"{src.Address.BuldingNo} - {src.Address.Street} - {src.Address.City}"));

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dist => dist.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuldingNo));

            CreateMap<TrainerToUpdateViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuldingNo = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.UpadateAt = DateTime.Now;
            });
        }
        private void MapSession()
        {
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, SessionViewModel>()
                        .ForMember(dest => dest.Catogry_Name, opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
                        .ForMember(dest => dest.Trainer_name, opt => opt.MapFrom(src => src.SessionTrainer.Name))
                        .ForMember(dest => dest.AvielableSlot, opt => opt.Ignore()); // Will Be Calculated After Map
            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();



        }
        private void MapMember()
        {
            CreateMap<CreateMemberViewmodel, Member>()
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                  {
                      BuldingNo = src.BuildingNumber,
                      City = src.City,
                      Street = src.Street
                  })).ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecord));


            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();
            CreateMap<Member, MemberViewModel>()
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DAteOfBirth.ToShortDateString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuldingNo} - {src.Address.Street} - {src.Address.City}"));

            CreateMap<Member, MemberToUpdateViewModel>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuldingNo))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuldingNo = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                    dest.UpadateAt = DateTime.Now;
                });
        }
        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();
            CreateMap<Plan, UpdatePlanViewModel>().ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Name));
            CreateMap<UpdatePlanViewModel, Plan>()
           .ForMember(dest => dest.Name, opt => opt.Ignore())
           .ForMember(dest => dest.UpadateAt, opt => opt.MapFrom(src => DateTime.Now));

        }
        private void MapBooking()
        {
            CreateMap<MemberSession, MemberSessionViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.CreatedAt.ToString()));

            CreateMap<CreateMemberSessionViewModel, MemberSession>();
            CreateMap<Member, MemberSelectViewModel>();


        }
    }
}
