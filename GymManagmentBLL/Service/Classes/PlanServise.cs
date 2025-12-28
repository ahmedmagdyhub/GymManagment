using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.PlanViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public  class PlanServise : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanServise(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlan()
        {
            var planes = _unitOfWork.GetRepository<Plan>().Getall();
            if (planes == null || !planes.Any()) return [];
            return planes.Select(P => new PlanViewModel 
            { 
            Id= P.Id ,
            Name =P.Name ,
            Description = P.Description,
            DurationDayes= P.DurationDays,
            IsActive = P.IsActive ,
            price = P.Price
            });

        }

        public PlanViewModel? GetPlanById(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan is null) return null;
            return new PlanViewModel
            {
                Id=plan .Id,
                Name =plan .Name ,
                Description =plan .Description ,
                DurationDayes=plan .DurationDays ,
                IsActive =plan .IsActive ,
                price =plan .Price 

            };
            
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int planid)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planid);
            if (plan is null || HasActive(planid)) return null;
            return new UpdatePlanViewModel 
            { 
            Description =plan .Description ,
            DurationDays =plan .DurationDays ,
            PlanName=plan .Name,
            Price=plan .Price
            
            
            };


        }

        public bool UpdatePlan(int planid, UpdatePlanViewModel PlanToUpdate)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planid);
            if (plan is null || HasActive(planid)) return false ;
            (plan.Description, plan.DurationDays, plan.Price, plan.Name) =
                (PlanToUpdate.Description, PlanToUpdate.DurationDays, PlanToUpdate.Price, PlanToUpdate.PlanName);
            _unitOfWork.GetRepository<Plan>().Upadte(plan);
            return _unitOfWork.SaveChange()>0;
        }
        public bool Tooglestation(int planid)
        {
         
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planid);
            if (plan is null || HasActive(planid)) return false ;
            plan .IsActive=plan.IsActive == true ?false : true;
            plan.CreatedAt = DateTime.Now;
            try
            {
                _unitOfWork.GetRepository<Plan>().Upadte(plan);
                return _unitOfWork.SaveChange() > 0;
            }
            catch { return false; }
        
        }

        
        private bool HasActive(int planid)
        {
            var activemembership = _unitOfWork.GetRepository<MemberShip>().Getall(x => x.PlanID == planid && x.Status == "active");
            return activemembership.Any();
        }
    }
}
