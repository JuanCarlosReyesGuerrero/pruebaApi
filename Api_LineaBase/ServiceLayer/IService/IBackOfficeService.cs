using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface IBackOfficeService
    {
        Task<Result> ValidateLogin(LoginModel objModel);

        Task<Result> ChangePassword(ChangePasswordModel objModel);
    }
}
