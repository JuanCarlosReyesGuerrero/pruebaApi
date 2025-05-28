using DomainLayer.Dtos;

namespace RepositoryLayer.IRepository
{
    public interface IPerfilRepository
    {
        Task<List<PerfilDto>> GetAll();
        Task<PerfilDto> GetById(int Id);
        Task<bool> Insert(PerfilDto entity);
        Task<bool> Update(PerfilDto entity);
        Task<bool> Delete(PerfilDto entity);
    }
}
