using DomainLayer.Dtos;

namespace RepositoryLayer.IRepository
{
    public interface IPermisoRepository
    {
        Task<List<PermisoDto>> GetAll();
        Task<PermisoDto> GetById(int Id);
        Task<bool> Insert(PermisoDto entity);
        Task<bool> Update(PermisoDto entity);
        Task<bool> Delete(PermisoDto entity);
    }
}
