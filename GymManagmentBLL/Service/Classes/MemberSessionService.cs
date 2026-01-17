using AutoMapper;
using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.MemberSessionViewModel;
using GymManagmentBLL.ViewModels.MemberViewMode_s;
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
    public class MemberSessionService : IMemberSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberSessionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork ;
            _mapper = mapper;
        }
        public bool CreateBooking(CreateMemberSessionViewModel createBooking)
        {
            if (createBooking == null)
                throw new Exception("createBooking is null!");

            var session = _unitOfWork.sessionReposatory.GetById(createBooking.SessionId);
            if (session == null || session.StartDate <= DateTime.Now)
                return false;
            if (_unitOfWork.MembershipRepository == null) throw new Exception("MembershipRepository is null!");

            var activeMembership = _unitOfWork.MembershipRepository
                .GetFirstOrDefault(m => m.MemberID == createBooking.MemberId && m.Status == "Active");
            
            var availableSlots = session.Capacity - _unitOfWork.sessionReposatory.GetCountOfBookedSlot(session.Id);
            if (availableSlots == 0) return false;

            var booking = _mapper.Map<MemberSession>(createBooking);
            booking.IsAttended = false;
            _unitOfWork.MemberSessionRepo.Add(booking);
            return _unitOfWork.SaveChange() > 0;


        }

        public IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory()
        {
            var sessions = _unitOfWork.sessionReposatory.GetAllSessionWithTrainerAndCategory();
            if (sessions == null) return [];

            var sessionVms = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in sessionVms)
                session.AvielableSlot = session.Capacity - _unitOfWork.sessionReposatory.GetCountOfBookedSlot(session.ID);
            return sessionVms;
        }

        public IEnumerable<MemberSessionViewModel> GetMemberForSession(int id)
        {
            var bookingRepo = _unitOfWork.MemberSessionRepo;
            var membersForSession = bookingRepo.GetSessionById(id);

            var memberForSession = _mapper.Map<IEnumerable<MemberSessionViewModel>>(membersForSession);
            return memberForSession;
        }
        public bool DeletaBooking(int MemberId)
        {
            var BookingRepo = _unitOfWork.MemberSessionRepo;
            var booking = BookingRepo.GetById(MemberId);
            if (booking == null) return false;
            BookingRepo.Delet(booking);
            return _unitOfWork.SaveChange() > 0;

        }
        public IEnumerable<MemberSelectViewModel> GetMembersForDropDown(int id)
        {
            var bookingRepo = _unitOfWork.MemberSessionRepo;
            var bookedMemberIds = bookingRepo.Getall(s => s.Id == id)
                .Select(b => b.MemberId)
                .ToList();
            var membersAvailable = _unitOfWork.GetRepository<Member>()
                .Getall(m => !bookedMemberIds.Contains(m.Id));
            var memberSelectVms = _mapper.Map<IEnumerable<MemberSelectViewModel>>(membersAvailable);
            return memberSelectVms;
        }
    }
}
