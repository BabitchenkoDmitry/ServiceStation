using System;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ServiceST.DAL.Entities;

namespace ServiceST.DAL.EF
{
    public partial class SSModel : DbContext
    {
        public SSModel(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.make)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.vin)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Car)
                .HasForeignKey(e => e.car_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Car)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.client_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.client_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderStatus>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<OrderStatus>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.OrderStatus)
                .HasForeignKey(e => e.orderstatus_id)
                .WillCascadeOnDelete(false);
        }
    }
}
