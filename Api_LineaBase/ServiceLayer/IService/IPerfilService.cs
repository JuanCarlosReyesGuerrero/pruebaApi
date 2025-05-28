using DomainLayer.Dtos;
using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface IPerfilService
    {
        Task<Result> GetAll();

        Task<Result> GetById(int Id);       

        Task<Result> Insert(PerfilDto entity);

        Task<Result> Update(PerfilDto entity);

        Task<Result> Delete(PerfilDto entity);
    }
}
