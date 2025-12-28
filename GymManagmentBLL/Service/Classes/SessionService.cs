using AutoMapper;
using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        

        

        public IEnumerable<SessionViewModel> GetAllSession()
        {
            var session = _unitOfWork.sessionReposatory.GetAllSessionWithTrainerAndCategory();
            if (!session.Any()) return [];
            //return session.Select(x => new SessionViewModel
            //{
            //    ID = x.Id,
            //    Catogry_Name = x.SessionCategory.CategoryName.ToString(),
            //    Capicity = x.Capacity,
            //    Description = x.Description,
            //    StartDate = x.StartDate,
            //    EndDate = x.EndDate,
            //    Trainer_name = x.SessionTrainer.Name,
            //    AvielableSlot = x.Capacity - _unitOfWork.sessionReposatory.GetCountOfBookedSlot(x.Id)

            //});
            var MappedSession = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(session);
            foreach (var sessions in MappedSession)
            {
                sessions.AvielableSlot = sessions.Capicity - _unitOfWork.sessionReposatory.GetCountOfBookedSlot(sessions.ID);
            }
            return MappedSession;
        }
        public IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown()
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().Getall();
            return trainer.Select(x => new TrainerSelectViewModel
            {
            Id=x.Id,
            Name =x.Name,

            });

            //return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainer);
        }

        public IEnumerable<CategorySelectViewModel> GetCategoryForDropDown()
        {
            var categry = _unitOfWork.GetRepository<Category>().Getall();
            return categry.Select(x => new CategorySelectViewModel
            {
                Id =x.Id,
                Name=x.CategoryName,
            });
            //return _mapper.Map<IEnumerable<CategorySelectViewModel>>(categry);
        }

        public SessionViewModel? GetSessionById(int sessionid)
        {
            var session = _unitOfWork.sessionReposatory.GetSessionWithTrainerAndCategory(sessionid);
            if (session == null) return null;
            //return new SessionViewModel
            //{
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    Trainer_name = session.SessionTrainer.Name,
            //    Catogry_Name = session.SessionCategory.CategoryName.ToString(),
            //    AvielableSlot = session.Capacity - _unitOfWork.sessionReposatory.GetCountOfBookedSlot(session.Id)

            //};
            var mappedsession = _mapper.Map<Session, SessionViewModel>(session);
            mappedsession.AvielableSlot = mappedsession.Capicity - _unitOfWork.sessionReposatory.GetCountOfBookedSlot(mappedsession.ID);
            return mappedsession;
        }

        public bool CreateSession(CreateSessionViewModel createSessionViewModel)
        {
            try
            {
                if (!IsTrainExist(createSessionViewModel.TrainerId)) return false;
                if (!IsCategoryExist(createSessionViewModel.CategoryId)) return false;
                if (!IsDateTimeValid(createSessionViewModel.StartDate, createSessionViewModel.EndDate)) return false;
                if (createSessionViewModel.Capacity > 25 || createSessionViewModel.Capacity <= 1) return false;
                var sessionEntity = _mapper.Map<Session>(createSessionViewModel);
                //var sessionEntity = new Session
                //{
                //    Description= createSessionViewModel.Description,
                //    TrainerId = createSessionViewModel.TrainerId,
                //    Capacity= createSessionViewModel.Capacity,
                //    CategoryId= createSessionViewModel.CategoryId,
                //    StartDate = createSessionViewModel.StartDate ,
                //    EndDate = createSessionViewModel.EndDate

                //};
                _unitOfWork.GetRepository<Session>().Add(sessionEntity);
                return _unitOfWork.SaveChange() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine( $"Create Session Failed {ex}");
                return false;
            }
        }
        public UpdateSessionViewModel? getSessionToUpdate(int sessionid)
        {
            var Session = _unitOfWork.sessionReposatory.GetById(sessionid);
            if (!IsSessionAvailableToUpdate(Session!)) return null;

            return _mapper.Map<UpdateSessionViewModel>(Session);
        }

        public bool UpdateSession(int sessionid, UpdateSessionViewModel updateSession)
        {
            try
            {
                var Session = _unitOfWork.sessionReposatory.GetById(sessionid);
                if (!IsSessionAvailableToUpdate(Session!)) return false;
                if (!IsTrainExist(updateSession.TrainerId)) return false;

                if (!IsDateTimeValid(updateSession.StartDate, updateSession.EndDate)) return false;

                _mapper.Map(updateSession, Session);
                Session!.UpadateAt = DateTime.Now;
                _unitOfWork.GetRepository<Session>().Upadte(Session);
                return _unitOfWork.SaveChange() > 0;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Faild {ex}");
                return false;
            }
        }

        public bool RemoveSession(int sessionid)
        {
            try
            {
                var session = _unitOfWork.sessionReposatory.GetById(sessionid);
                if (!IsSessionAvailableRemove(session!)) return false;

                _unitOfWork.sessionReposatory.Delet(session!);
                return _unitOfWork.SaveChange() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove Session Faild {ex}");
                return false;
            }
        }

        #region helper methods
        private bool IsTrainExist(int trainerID)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerID) is not null;
        }
        private bool IsCategoryExist(int CategoryID)
        {
            return _unitOfWork.GetRepository<Category>().GetById(CategoryID) is not null;
        }
        private bool IsDateTimeValid(DateTime startdate, DateTime enddate)
        {
            return enddate > startdate;
        }
        private bool IsSessionAvailableToUpdate(Session session)
        {

            if (session is null) return false;

            if (session.EndDate < DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now) return false;

            var HasactiveBooking = _unitOfWork.sessionReposatory.GetCountOfBookedSlot(session.Id) > 0;
            if (HasactiveBooking) return false;
            return true;



           }
        private bool IsSessionAvailableRemove(Session session)
        {
            if (session is null) return false;
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;
            if (session.StartDate > DateTime.Now) return false;
            var hasactivebooking = _unitOfWork.sessionReposatory.GetCountOfBookedSlot(session.Id) > 0;
            if (hasactivebooking) return false;
            return true;
        }






        #endregion
    }
}
