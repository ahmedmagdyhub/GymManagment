

namespace GymManagmentBLL.ViewModels.PlanViewModels;

public  class PlanViewModel
{
    public  int Id { get; set; }
    public string Name { get; set; } = null!;
    public  int DurationDayes { get; set; }

    public  string?  Description { get; set; }

    public  decimal  price { get; set; }

    public  bool IsActive { get; set; }
}
