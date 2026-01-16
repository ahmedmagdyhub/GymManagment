using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.MemberPlan;
using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using GymManagmentDAL.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public class MemberPlanService : IMemberPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberPlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MemberPlanViewModel> GetAll()
        {
            var memberShips = _unitOfWork.GetRepository<MemberShip>().Getall().ToList();

            var members = _unitOfWork.GetRepository<Member>().Getall().ToList();

            var plans = _unitOfWork.GetRepository<Plan>().Getall().ToList();

            return memberShips.Select(mp => new MemberPlanViewModel
            {
                Id = mp.Id,

                MemberName = members
                    .FirstOrDefault(m => m.Id == mp.MemberID)?.Name ?? "—",

                PlanName = plans
                    .FirstOrDefault(p => p.Id == mp.PlanID)?.Name ?? "—",

                StartDate = mp.CreatedAt,
                EndDate = mp.EndDate,

                Status = mp.EndDate > DateTime.Now ? "Active" : "Expired"
            });
        }

        public CreateMemberPlanViewModel GetCreateFormData()
        {
            var memberrepo = _unitOfWork.GetRepository<Member>();
            var planrepo = _unitOfWork.GetRepository<Plan>();
            return new CreateMemberPlanViewModel
            {
                Members = memberrepo.Getall()
                    .Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Name
                    }),

                Plans = planrepo.Getall(p=>p.IsActive)
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    })
            };
        }

        public bool Create(CreateMemberPlanViewModel model)
        {
            var memberrepo = _unitOfWork.GetRepository<Member>();
            var Planrepo = _unitOfWork.GetRepository<Plan>();
            var memberPlanRepo = _unitOfWork.GetRepository<MemberShip>();
            var member = memberrepo.GetById(model.MemberId);
            if (member == null)
                return false;

            var plan = Planrepo
     .Getall(p => p.Id == model.PlanId && p.IsActive)
     .FirstOrDefault();

            if (plan == null)
                return false;

            bool hasActiveMembership = memberPlanRepo
       .Getall(mp => mp.MemberID == model.MemberId && mp.EndDate > DateTime.Now) .Any();
            if (hasActiveMembership)
                return false;

            var memberPlan = new MemberShip
            {
                MemberID = model.MemberId,
                PlanID = model.PlanId,
                CreatedAt = DateTime.Now,
                EndDate = DateTime.Now.AddDays(plan.DurationDays)
            };

            memberPlanRepo.Add(memberPlan);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool Cancel(int memberPlanId)
        {
            var memberPlanRepo = _unitOfWork.GetRepository<MemberShip>();

            var membership = memberPlanRepo.GetById(memberPlanId);
            if (membership == null)
                return false;

            if (membership.EndDate <= DateTime.Now)
                return false;

            memberPlanRepo.Delet(membership);

            _unitOfWork.SaveChange();

            return true;
        }
    }
}

