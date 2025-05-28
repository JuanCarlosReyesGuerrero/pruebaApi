using DomainLayer.Dtos;
using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface ITipoDocumentoService
    {
        Task<Result> GetAll();
        Task<Result> GetById(int Id);
        Task<Result> Insert(TipoDocumentoDto entity);
        Task<Result> Update(TipoDocumentoDto entity);
        Task<Result> Delete(TipoDocumentoDto entity);
    }
}
