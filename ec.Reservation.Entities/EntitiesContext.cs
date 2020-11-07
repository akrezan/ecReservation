using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ec.Reservation.Entities
{
    
   public class EntitiesContext : DbContext
    {
        public EntitiesContext() : base("ReservationDB")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Reservation>().
                HasMany(c => c.Attendees).
                WithMany(p => p.Reservations).
                Map(
                    m =>
                    {
                        m.MapLeftKey("ReservationId");
                        m.MapRightKey("UserId");
                        m.ToTable("UserReservations");
                    });
        }

    }
}
