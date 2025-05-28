using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface IAuthToken
    {
        string GenerarToken(AuthModel authModel);
    }
}
