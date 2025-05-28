using DomainLayer.Dtos;

namespace RepositoryLayer.IRepository
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioDto>> GetAll();
        Task<UsuarioDto> GetById(int id);
        Task<bool> Insert(UsuarioDto entity);
        Task<bool> Update(UsuarioDto entity);
        Task<bool> Delete(UsuarioDto entity);
        Task<UsuarioDto> GetByIdPassword(int id);
        Task<UsuarioDto> GetByIdEliminado(string vNumeroDocumento);
    }
}
