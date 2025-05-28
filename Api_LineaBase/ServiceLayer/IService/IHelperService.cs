using DomainLayer.Dtos;
using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface IHelperService
    {
        Task<Result> UploadImagen(UploadFileDto objFile);
    }
}
