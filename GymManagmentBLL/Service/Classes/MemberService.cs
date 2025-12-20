using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.MemberViewMode_s;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateMember(CreateMemberViewmodel createMemberViewmodel)
        {
            try
            {
                if (CheckEmailExist(createMemberViewmodel.Email) || CheckphoneExist(createMemberViewmodel.Phone))
                    return false;
                var member = new Member()
                {
                    Name = createMemberViewmodel.Name,
                    Email = createMemberViewmodel.Email,
                    Phone = createMemberViewmodel.Phone,
                    Gender = createMemberViewmodel.Gender,
                    DAteOfBirth = createMemberViewmodel.DateOfBirth,
                    Address = new Address()
                    {
                        City = createMemberViewmodel.City,
                        Street = createMemberViewmodel.Street,
                        BuldingNo = createMemberViewmodel.BuildingNumber
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Weight = createMemberViewmodel.HealthRecord.Weight,
                        Height = createMemberViewmodel.HealthRecord.Height,
                        BloodType = createMemberViewmodel.HealthRecord.BloodType,
                        Note = createMemberViewmodel.HealthRecord.Note
                    }

                }; _unitOfWork.GetRepository<Member >().Add(member) ;
                return _unitOfWork.SaveChange()>0;


            }
            catch (Exception )
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMember()
        {
            var members = _unitOfWork.GetRepository<Member>().Getall();
            if (members is null || members.Any()) return [];
            var MemberViewmodel = new List<MemberViewModel>();
            foreach (var member in members)
            {
                var Memberviewmodel = new MemberViewModel()
                {
                    Name = member.Name ,
                    Id = member .Id ,
                    photo = member .Photo ,
                    Email = member .Email ,
                    Phone = member .Phone,
                    Gender = member .Gender .ToString ()

                };
                MemberViewmodel.Add(Memberviewmodel);
            }
            return MemberViewmodel;
        }

        public MemberViewModel? getMemberDeatails(int memberid)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberid);
            if (member is null) return null; 
            var viewmodel = new MemberViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                photo = member.Photo,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                Address = $"{member.Address.BuldingNo} , {member.Address.Street} , {member.Address.City}",
                DateOfBirth = member.DAteOfBirth.ToShortDateString()
            };
            var activemembership = _unitOfWork.GetRepository<MemberShip>().Getall(X => X.Id == memberid && X.Status == "Active").FirstOrDefault();
            if (activemembership is not null)
            {
                viewmodel.MemberShipStartDate = activemembership.CreatedAt.ToShortDateString();
                viewmodel.MemberShipEndDate = activemembership.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository <Plan>().GetById(activemembership.PlanID);
                viewmodel.PlanName = plan?.Name;
            } return viewmodel;

        }

        public HealthRecordViewModel? GetMemberHealthRecordDeatails(int memberid)
        {
            var memberhealthrecord = _unitOfWork.GetRepository <HealthRecord>().GetById(memberid);
            if (memberhealthrecord is null) return null;
            return new HealthRecordViewModel()
            {
                BloodType = memberhealthrecord.BloodType,
                Height = memberhealthrecord.Height,
                Weight = memberhealthrecord.Weight,
                Note = memberhealthrecord.Note
            };
      }

        public MemberToUpdateViewModel? GetMemberToUpdate(int memberid)
        {
            var member = _unitOfWork.GetRepository <Member>().GetById(memberid);
            if (member is null) return null;
            return new MemberToUpdateViewModel()
            {
                Email = member.Email,
                Name = member.Name,
                Phone = member.Phone,
                Photo = member .Photo ,
                City = member .Address .City ,
                Street= member.Address .Street ,
                BuildingNumber = member .Address .BuldingNo 
            };
        }

        public bool MemberToUpdate(int memberid, MemberToUpdateViewModel membertoupdate)
        {
            try
            {
                if (CheckEmailExist(membertoupdate.Email) || CheckphoneExist(membertoupdate.Phone))
                    return false;

                var member = _unitOfWork.GetRepository <Member>().GetById(memberid);
                if (member is null) return false;
                member.Email = membertoupdate.Email;
                member.Phone = membertoupdate.Phone;
                member.Photo = membertoupdate.Photo;
                member.Address.City = membertoupdate.City;
                member.Address.Street = membertoupdate.Street;
                member.Address.BuldingNo = membertoupdate.BuildingNumber;
                member.CreatedAt = DateTime.Now;
                _unitOfWork.GetRepository <Member >().Upadte(member) ;
                return _unitOfWork.SaveChange()>0;
            }
            catch
            {
                return false;
            }
        }

        public bool Removemember(int memberid)
        {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var member = memberRepo.GetById(memberid);
            if (member is null) return false;
            var membersessionRepo = _unitOfWork.GetRepository<MemberSession>();
            var hasactivememberSession = membersessionRepo.Getall(x => x.MemberId == memberid && x.Session.StartDate > DateTime.Now).Any ();
            if (hasactivememberSession) return false ;
            var membershipRepo = _unitOfWork.GetRepository<MemberShip>();
            var Membership = membershipRepo.Getall(x => x.MemberID == memberid);
            try
            {
                if(Membership .Any())
                {
                    foreach (var membership in Membership)
                    {
                        membershipRepo.Delet(membership);

                    }
                }
                 memberRepo.Delet(member);
                return _unitOfWork.SaveChange() > 0;
            }
            catch
            {
                return false;
            }
        }
        #region helper exist 
        private bool CheckEmailExist(string email)
        {
            return  _unitOfWork.GetRepository <Member >().Getall(X => X.Email == email).Any();
        }
        private bool CheckphoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().Getall(X => X.Phone == phone).Any();
        }
        #endregion 
    }
}
