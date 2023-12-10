using System.ComponentModel.DataAnnotations;

namespace AcariPlanillaAPI.Models
{
    public class HistorialActualizacionPlanillas
    {
        [Key]
        public int HistorialId { get; set; }
        public required Boletas Boletas { get; set; }
        public required string RutaDocumento {  get; set; }
        public required DateTime FechaActualizacion {  get; set; }
    }
}
