using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public interface IOpinionesService
    {
        Task<List<OpinionesResponseDto>> GetAllAsync();
        Task<OpinionesResponseDto?> GetByIdAsync(int id);
        Task<OpinionesResponseDto> CreateAsync(OpinionesRequestDto request);
        Task<OpinionesResponseDto?> UpdateAsync(int id, OpinionesRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}