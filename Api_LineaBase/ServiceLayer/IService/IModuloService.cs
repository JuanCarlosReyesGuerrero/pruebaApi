using DomainLayer.Dtos;
using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface IModuloService
    {
        Task<Result> GetAll();
        Task<Result> GetById(int Id);
        Task<Result> Insert(ModuloDto entity);
        Task<Result> Update(ModuloDto entity);
        Task<Result> Delete(ModuloDto entity);
    }
}
