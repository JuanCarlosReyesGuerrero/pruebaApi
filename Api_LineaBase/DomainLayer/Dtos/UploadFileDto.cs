using Microsoft.AspNetCore.Http;

namespace DomainLayer.Dtos
{
    public class UploadFileDto
    {
        public IFormFile? File { get; set; }
        public string? RutaRaiz { get; set; }
        public string? NombreCarpeta { get; set; }
    }
}
