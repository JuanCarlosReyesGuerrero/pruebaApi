using DomainLayer.Dtos;

namespace RepositoryLayer.IRepository
{
    public interface ITipoDocumentoRepository
    {
        Task<List<TipoDocumentoDto>> GetAll();
        Task<TipoDocumentoDto> GetById(int Id);
        Task<bool> Insert(TipoDocumentoDto entity);
        Task<bool> Update(TipoDocumentoDto entity);
        Task<bool> Delete(TipoDocumentoDto entity);
    }
}
