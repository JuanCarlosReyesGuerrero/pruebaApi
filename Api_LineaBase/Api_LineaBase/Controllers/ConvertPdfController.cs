using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace Api_Empopasto.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertPdfController : ControllerBase
    {
        private readonly ICreateLogger createLogger;
        private readonly IWebHostEnvironment _env;
        private readonly IConvertPdfService _objService;
        private readonly IHelperService _objHelperService;

        public ConvertPdfController(IConvertPdfService objService, IWebHostEnvironment env, IHelperService objHelperService, ICreateLogger _createLogger)
        {
            _objService = objService;
            _env = env;
            _objHelperService = objHelperService;
            this.createLogger = createLogger;
        }

        [HttpPost, Route("CreatePdfByImage")]
        public async Task<Result> CreatePdfByImage(string Consecutivo)
        {
            Result oRespuesta = new Result();
            Result vTemp = new Result();
            Result vTemp1 = new Result();

            ConvertPdfDto convertPdfDto = new ConvertPdfDto();

            DateTime vFecha = DateTime.Now;

            Guid objGuid = new Guid();

            string vNombreArchivo = objGuid.ToString()+ vFecha.ToString("yyyyMMdd") + Consecutivo.PadLeft(2, '0') + ".pdf";
            string vConsecutivo = Consecutivo.PadLeft(2, '0');

            try
            {
                var file = Request.Form.Files[0];
                string vRutaRaiz = this._env.WebRootPath;
                string vNombreCarpeta = "/ImagenesLectura/";

                UploadFileDto objFile = new UploadFileDto();

                objFile.File = file;
                objFile.RutaRaiz = vRutaRaiz;
                objFile.NombreCarpeta = vNombreCarpeta;

                vTemp = await _objHelperService.UploadImagen(objFile);

                if (vTemp.Success)
                {
                    convertPdfDto.ArchivoOrigen = vTemp.MessageHttp;
                    convertPdfDto.ArchivoDestino = vRutaRaiz + vNombreCarpeta + vNombreArchivo;
                    convertPdfDto.CodigoGuid = objGuid.ToString();
                    convertPdfDto.Fechalectura = vFecha.ToString();
                    convertPdfDto.Consecutivo = vConsecutivo;

                    vTemp1 = await _objService.ConvertImageToPdf(convertPdfDto);

                    if (vTemp1.Success)
                    {
                        string host = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/ImagenesLectura/{vNombreArchivo}";

                        oRespuesta.Success = vTemp1.Success;
                        oRespuesta.MessageHttp = host;
                    }
                    else
                    {
                        oRespuesta.MessageHttp = vTemp1.MessageHttp;
                    }
                }
                else
                {
                    oRespuesta.MessageHttp = vTemp.MessageHttp;
                }
            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);

                oRespuesta.MessageHttp = ex.Message;
            }

            return oRespuesta;
        }
    }
}
