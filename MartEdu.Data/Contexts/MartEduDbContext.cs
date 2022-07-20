using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;


namespace MartEdu.Data.Contexts
{
    public class MartEduDbContext : DbContext
    {
        public MartEduDbContext(DbContextOptions<MartEduDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasOne(e => e.Author).WithMany(c => c.Courses);
            modelBuilder.Entity<Author>().HasMany(c => c.Courses).WithOne(e => e.Author);


            modelBuilder.Entity<User>().HasMany(u => u.Courses).WithMany(c => c.Participants);
            modelBuilder.Entity<Course>().HasMany(u => u.Participants).WithMany(c => c.Courses);
        }
    }
}
