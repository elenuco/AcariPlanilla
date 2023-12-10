namespace AcariPlanillaAPI.Models.DTO
{
    public class BoletasDto
    {
        public class BoletaDto
        {
            public required string UserName { get; set; }
            public required string CodigoEmpleado { get; set; }
            public DateTime Corte { get; set; }
            public float DescuentoAFP { get; set; }
            public float DescuentoISSS { get; set; }
            public float DescuentoRenta { get; set; }
            public float SueldoNeto { get; set; }
        }
    }
}
