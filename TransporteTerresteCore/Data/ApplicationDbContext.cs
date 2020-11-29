using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TransporteTerresteCore.Data.Entities;

namespace TransporteTerresteCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<ReservaTransporteTerrestre> ReservaTransporteTerrestres { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}
