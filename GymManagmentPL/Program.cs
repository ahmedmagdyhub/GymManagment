using GymManagmentBLL;
using GymManagmentBLL.Service.Classes;
using GymManagmentBLL.Service.InterFaces;
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.DataSeeding;
using GymManagmentDAL.Repository;
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
            builder.Services.AddScoped<IAnalyticsServise, AnalyticsServise>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerServise, TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanServise>();
            builder.Services.AddScoped<ISessionService, SessionService>();

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            var app = builder.Build();
            #region Dataseeding
            using var scope = app.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<GymManagmentDbContext>();
            var pendingmigarions = dbcontext.Database.GetPendingMigrations();
            if(pendingmigarions ?.Any() ?? false)
            {
                dbcontext.Database.Migrate();
            }
            GymDbContextSeeding.SeedData(dbcontext, app.Environment.ContentRootPath);
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

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
