﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FusionWeb.Models;

namespace FusionWeb.Data
{
    public class FusionWebContext : DbContext
    {
        public FusionWebContext (DbContextOptions<FusionWebContext> options)
            : base(options)
        {
        }

        public DbSet<FusionWeb.Models.Client> Client { get; set; }

        public DbSet<FusionWeb.Models.Contact> Contact { get; set; }

        public DbSet<FusionWeb.Models.Dish> Dish { get; set; }

        public DbSet<FusionWeb.Models.DishOrder> DishOrder { get; set; }

        //public DbSet<FusionWeb.Models.Kitchen> Kitchen { get; set; }


        public DbSet<FusionWeb.Models.Manager> Manager { get; set; }

        public DbSet<FusionWeb.Models.Order> Order { get; set; }

        public DbSet<FusionWeb.Models.Reservation> Reservasion { get; set; }

        public DbSet<FusionWeb.Models.Cart> Cart { get; set; }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Reservation>()
                .HasRequired<Client>(r => r.Client)
                .WithMany(g => g.Students)
                .HasForeignKey<int>(s => s.CurrentGradeId);
        }*/
    }

}

