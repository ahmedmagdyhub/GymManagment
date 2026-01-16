using GymManagmentBLL;
using GymManagmentBLL.Service.Classes;
using GymManagmentBLL.Service.Classes.AttachmentService;
using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.Service.InterFaces.AttachmentService;
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.DataSeeding;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymManagmentDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"));      
            });
            //builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            //builder.Services.AddScoped<IPlanRepo, PlanRepo>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepo, SessionRepo>();
            builder.Services.AddScoped<IMemberSessionRepo, MemberSessionRepo>();
            builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
            builder.Services.AddScoped<IMemberSessionRepo, MemberSessionRepo>();
            builder.Services.AddScoped<IAnalyticsServise, AnalyticsServise>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerServise, TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanServise>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder .Services.AddScoped<IAccountService ,AccountService >();
            builder.Services.AddScoped<IMemberPlanService, MemberPlanService>();
            builder.Services.AddScoped<IMemberSessionService, MemberSessionService>();

            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(confg =>
            {
                //confg.Password.RequireUppercase=true;
                //confg.Password.RequireLowercase=true;
                //confg.Password.RequiredLength = 6;
                confg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<GymManagmentDbContext>();
            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/Login";
                option.AccessDeniedPath = "/Account/AccessDenied";
            });

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            var app = builder.Build();
            #region Dataseeding
            using var scope = app.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<GymManagmentDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var pendingmigarions = dbcontext.Database.GetPendingMigrations();
            if(pendingmigarions ?.Any() ?? false)
            {
                dbcontext.Database.Migrate();
            }
            GymDbContextSeeding.SeedData(dbcontext, app.Environment.ContentRootPath);
            IdentityDbContextSeeding.SeedData(roleManager, userManager);

            #endregion 

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id:?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
