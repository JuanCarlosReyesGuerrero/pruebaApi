using DomainLayer.Dtos;
using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IBackOfficeRepository
    {
        Task<UsuarioDto> GetUsuarioByPassword(ChangePasswordModel objModel);
        Task<UsuarioDto> ValidateLogin(LoginModel objModel);
        Task<bool> ChangePassword(ChangePasswordModel objModel);
        Task<bool> BlockUser(LoginModel objModel);
        Task<bool> UpdateAttempts(LoginModel objModel);
        Task<bool> Update(ChangePasswordModel objModel);
    }
}
