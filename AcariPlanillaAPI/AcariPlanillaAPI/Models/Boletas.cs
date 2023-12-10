using AcariPlanillaAPI.Models;
using System.ComponentModel.DataAnnotations;
namespace AcariPlanillaAPI.Models
{
    public class Boletas
    {
        [Key]
        public int BoletaId { get; set; }
        //public required Usuarios Usuarios { get; set; }
        public required int UserId {  get; set; }
        public required string CodigoEmpleado {  get; set; }
        public DateOnly Corte {  get; set; }
        public required decimal DescuentoISSS {  get; set; }
        public required decimal DescuentoAFP {  get; set; }
        public required decimal DescuentoRenta {  get; set; }
        public required decimal SueldoNeto{  get; set; }
    }
}
