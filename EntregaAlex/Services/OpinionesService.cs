using EntregaAlex.Models;
using EntregaAlex.Repository;
using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public class OpinionesService : IOpinionesService
    {
        private readonly IOpinionesRepository _repository;

        public OpinionesService(IOpinionesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OpinionesResponseDto>> GetAllAsync()
        {
            var lista = await _repository.GetAllAsync();
            return lista.Select(e => new OpinionesResponseDto
            {
                Id = e.Id,
                NombreCompleto = e.NombreCompleto,
                FechaCreacion = e.FechaCreacion,
                Puntuacion = e.Puntuacion,
                Mensaje = e.Mensaje,
            }).ToList();
        }

        public async Task<OpinionesResponseDto?> GetByIdAsync(int id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;

            return new OpinionesResponseDto
            {
                Id = e.Id,
                NombreCompleto = e.NombreCompleto,
                FechaCreacion = e.FechaCreacion,
                Puntuacion = e.Puntuacion,
                Mensaje = e.Mensaje,
            };
        }

        public async Task<OpinionesResponseDto> CreateAsync(OpinionesRequestDto request)
        {
            var nuevo = new Opiniones
            {
                NombreCompleto = request.NombreCompleto,
                FechaCreacion = DateTime.Now,
                Puntuacion = request.Puntuacion,
                Mensaje = request.Mensaje,
            };

            var creado = await _repository.CreateAsync(nuevo);

            return new OpinionesResponseDto
            {
                Id = creado.Id,
                NombreCompleto = creado.NombreCompleto,
                FechaCreacion = creado.FechaCreacion,
                Puntuacion = creado.Puntuacion,
                Mensaje = creado.Mensaje,
            };
        }

        public async Task<OpinionesResponseDto?> UpdateAsync(int id, OpinionesRequestDto request)
        {
            var opiniones = new Opiniones
            {
                Id = id,
                NombreCompleto = request.NombreCompleto,
                FechaCreacion = DateTime.Now,
                Puntuacion = request.Puntuacion,
                Mensaje = request.Mensaje,
            };

            var actualizado = await _repository.UpdateAsync(opiniones);
            if (actualizado == null) return null;

            return new OpinionesResponseDto
            {
                Id = actualizado.Id,
                NombreCompleto = actualizado.NombreCompleto,
                FechaCreacion = actualizado.FechaCreacion,
                Puntuacion = actualizado.Puntuacion,
                Mensaje = actualizado.Mensaje,
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}