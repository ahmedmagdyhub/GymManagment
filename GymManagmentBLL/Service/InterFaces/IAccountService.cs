using GymManagmentBLL.ViewModels.AccountViewModel;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.InterFaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidateUser(LoginViewModel loginViewModel);
    }
}
