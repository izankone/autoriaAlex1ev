using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Opiniones
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string NombreCompleto { get; set; } = string.Empty;
        
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "La puntuacion debe de ser entre 1 y 5")]
        public int Puntuacion { get; set; } 
        
        [Required(ErrorMessage = "No puedes dejar el campo vacio")]
        public string Mensaje { get; set; } = string.Empty;

        public int EventoId { get; set; }

        [JsonIgnore]
        public Evento? Evento { get; set; } 
    }
}