using DomainLayer.Dtos;
using DomainLayer.Models;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class HelperService : IHelperService
    {
        public async Task<Result> UploadImagen(UploadFileDto objFile)
        {
            Result oRespuesta = new Result();

            try
            {
                string RutaCompleta = objFile.RutaRaiz + objFile.NombreCarpeta;

                if (!Directory.Exists(RutaCompleta))
                {
                    Directory.CreateDirectory(RutaCompleta);
                }

                if (objFile.File.Length > 0)
                {
                    string NombreArchivo = objFile.File.FileName;

                    string RutaFullCompleta = Path.Combine(RutaCompleta, NombreArchivo);

                    using (var stream = new FileStream(RutaFullCompleta, FileMode.Create))
                    {
                        objFile.File.CopyTo(stream);

                        oRespuesta.Success = true;
                    }

                    oRespuesta.MessageHttp = RutaFullCompleta;
                    oRespuesta.Success=true;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.MessageHttp = ex.Message;
                oRespuesta.Success = false;
            }

            return oRespuesta;
        }
    }
}
