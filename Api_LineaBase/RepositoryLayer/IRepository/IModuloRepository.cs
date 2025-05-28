using DomainLayer.Dtos;

namespace RepositoryLayer.IRepository
{
    public interface IModuloRepository
    {
        Task<List<ModuloDto>> GetAll();
        Task<ModuloDto> GetById(int Id);
        Task<bool> Insert(ModuloDto entity);
        Task<bool> Update(ModuloDto entity);
        Task<bool> Delete(ModuloDto entity);
    }
}
