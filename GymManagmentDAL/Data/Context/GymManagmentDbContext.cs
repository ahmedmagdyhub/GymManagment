using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Context
{
    public  class GymManagmentDbContext : DbContext
    {
        public GymManagmentDbContext(DbContextOptions<GymManagmentDbContext> option ): base (option )
        {
             
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymManagment;Trusted_Connection=True;TrustServerCertificate=true");

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public  DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public  DbSet<Category > Categories { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }

        public DbSet<MemberSession> MemberSessions { get; set; }

    }
}
