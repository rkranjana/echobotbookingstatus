using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;


#nullable disable

namespace EchoBotDBbookingstatus.Models
{
    public partial class BookingstatusDBContext : DbContext
    {
        public BookingstatusDBContext()
        {
        }

        public BookingstatusDBContext(DbContextOptions<BookingstatusDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bookingdetail> Bookingdetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionstring = config.GetConnectionString("DefaultConnection");
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(connectionstring);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bookingdetail>(entity =>
            {
                entity.HasKey(e => e.Bookingid)
                    .HasName("pk_bookingid");

                entity.ToTable("BOOKINGDETAILS");

                entity.Property(e => e.Bookingid)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.From)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Venue)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
