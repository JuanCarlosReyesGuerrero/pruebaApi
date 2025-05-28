using DomainLayer.Dtos;
using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface IConvertPdfService
    {
        Task<Result> ConvertImageToPdf(ConvertPdfDto convertPdfDto);        
    }
}
