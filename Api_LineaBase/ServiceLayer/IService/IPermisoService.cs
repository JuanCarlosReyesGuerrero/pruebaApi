using DomainLayer.Dtos;
using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface IPermisoService
    {
        Task<Result> GetAll();
        Task<Result> GetById(int Id);
        Task<Result> Insert(PermisoDto entity);
        Task<Result> Update(PermisoDto entity);
        Task<Result> Delete(PermisoDto entity);

        Task<Result> GetAllFull();
        Task<Result> GetAllFullByIdUsuario(int Id);
    }
}
