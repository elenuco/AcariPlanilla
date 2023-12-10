using AcariPlanillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AcariPlanillaAPI.Context
{
    public class PlanillaDbContext(DbContextOptions<PlanillaDbContext> options) : DbContext(options)
    {
        public DbSet<Usuarios>Usuarios { get; set; }
        public DbSet<Boletas> Boletas { get; set; }
        public DbSet<HistorialActualizacionPlanillas> HistorialActualizacionPlanillas { get; set; }
    }
}
