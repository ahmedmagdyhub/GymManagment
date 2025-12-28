using AutoMapper;
using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public  class TrainerService : ITrainerServise
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                var Repo = _unitOfWork.GetRepository<Trainer>();

                if (IsEmailExists(createTrainer.Email) || IsPhoneExists(createTrainer.Phone)) return false;
                var TrainerEntity = _mapper.Map<CreateTrainerViewModel, Trainer>(createTrainer);


                Repo.Add(TrainerEntity);

                return _unitOfWork.SaveChange() > 0;


            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().Getall();
            if (Trainers is null || !Trainers.Any()) return [];

            var mappedTrainers = _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerViewModel>>(Trainers);
            return mappedTrainers;
        }
        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (Trainer is null) return null;

            var mappedTrainer = _mapper.Map<Trainer, TrainerViewModel>(Trainer);
            return mappedTrainer;
        }
        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (Trainer is null) return null;

            var mappedTrainer = _mapper.Map<Trainer, TrainerToUpdateViewModel>(Trainer);
            return mappedTrainer;



        }
        public bool RemoveTrainer(int trainerId)
        {
            var Repo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToRemove = Repo.GetById(trainerId);
            if (TrainerToRemove is null || HasActiveSessions(trainerId)) return false;
            Repo.Delet(TrainerToRemove);
            return _unitOfWork.SaveChange() > 0;

        }
        public bool UpdateTrainerDetails(TrainerToUpdateViewModel updatedTrainer, int trainerId)
        {
            var emailExist = _unitOfWork.GetRepository<Trainer>().Getall(
                m => m.Email == updatedTrainer.Email && m.Id != trainerId);

            var PhoneExist = _unitOfWork.GetRepository<Trainer>().Getall(
                m => m.Phone == updatedTrainer.Phone && m.Id != trainerId);

            if (emailExist.Any() || PhoneExist.Any()) return false;

            var Repo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToUpdate = Repo.GetById(trainerId);

            if (TrainerToUpdate is null) return false;

            _mapper.Map(updatedTrainer, TrainerToUpdate);
            TrainerToUpdate.UpadateAt = DateTime.Now;

            return _unitOfWork.SaveChange() > 0;
        }

        #region Helper Methods
        private bool IsEmailExists(string email)
        {
            var existing = _unitOfWork.GetRepository<Trainer>().Getall(
                m => m.Email == email).Any();
            return existing;
        }
        private bool IsPhoneExists(string phone)
        {
            var existing = _unitOfWork.GetRepository<Trainer>().Getall(
                m => m.Phone == phone).Any();
            return existing;
        }
        private bool HasActiveSessions(int Id)
        {
            var activeSessions = _unitOfWork.GetRepository<Session>().Getall(
               s => s.TrainerId == Id && s.StartDate > DateTime.Now).Any();
            return activeSessions;
        }
        #endregion
    }
}
