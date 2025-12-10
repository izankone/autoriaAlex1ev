using System.ComponentModel.DataAnnotations;

namespace EntregaAlex.Dtos
{
    public class OpinionesRequestDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de creacion es obligatorio")]
        public DateTime FechaCreacion{ get; set; }

        [Range(1, 5, ErrorMessage = "La putuación debe de estar entre el 1 y el 5")]
        public int Puntuacion { get; set; } 
        
        public string Mensaje { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debes indicar a qué evento pertenece esta opinión")]
        public int EventoId { get; set; }
    }

    public class OpinionesResponseDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int Puntuacion { get; set; } 
        public string Mensaje { get; set; } = string.Empty;
        // Opcional: devolver también el ID
        public int EventoId { get; set; } 
    }
}