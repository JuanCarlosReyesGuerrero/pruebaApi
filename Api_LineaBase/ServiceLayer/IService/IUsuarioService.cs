using DomainLayer.Dtos;
using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface IUsuarioService
    {
        Task<Result> GetAll();
        Task<Result> GetById(int id);
        Task<Result> Insert(UsuarioDto entity);
        Task<Result> Update(UsuarioDto entity);
        Task<Result> Delete(UsuarioDto entity);
        Task<Result> GetByIdPassword(int id);
        Task<Result> UpdateEliminado(UsuarioDto entity);
    }
}
