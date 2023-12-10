using System.ComponentModel.DataAnnotations;

namespace AcariPlanillaAPI.Models
{
    public class Usuarios
    {
        [Key]
        public int UserId { get; set; }
        public required string UserName {  get; set; }
        public required string PasswordHash {  get; set; }
    }
}
