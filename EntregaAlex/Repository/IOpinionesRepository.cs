using EntregaAlex.Models;

namespace EntregaAlex.Repository
{
    public interface IOpinionesRepository
    {
        Task<List<Opiniones>> GetAllAsync();
        Task<Opiniones?> GetByIdAsync(int id);
        Task<Opiniones> CreateAsync(Opiniones evento);
        Task<Opiniones?> UpdateAsync(Opiniones evento);
        Task<bool> DeleteAsync(int id);
    }
}