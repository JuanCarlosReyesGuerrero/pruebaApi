using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Extensions.Configuration;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class ConvertPdfService : IConvertPdfService
    {
        private readonly IConfiguration configuration;
        private readonly ICreateLogger createLogger;

        public ConvertPdfService(IConfiguration _configuration, ICreateLogger _createLogger)
        {
            this.configuration = _configuration;
            this.createLogger = _createLogger;
        }

        public async Task<Result> ConvertImageToPdf(ConvertPdfDto objModel)
        {
            Result oRespuesta = new Result();

            try
            {
                var CarpetaOrigen = objModel.ArchivoOrigen;
                var CarpetaDestino = objModel.ArchivoDestino;

                ImageData imageData = ImageDataFactory.Create(CarpetaOrigen);
                PdfDocument pdfDocument = new PdfDocument(new PdfWriter(CarpetaDestino));
                Document document = new Document(pdfDocument);

                //****************** METADATA ***********************************

                PdfDocumentInfo info = pdfDocument.GetDocumentInfo();

                IDictionary<String, String> newInfo = new Dictionary<String, String>();

                newInfo.Add("Fecha Creación: ", objModel.Fechalectura);
                newInfo.Add("Codigo Guid: ", objModel.CodigoGuid);
                newInfo.Add("Consecutivo: ", objModel.Consecutivo);

                newInfo.Add("Autor: ", "NombrePrograma");
                newInfo.Add("Título: ", "Imagen Convertida a Pdf");
                newInfo.Add("Tema: ", "Evidencia de la suscripción " + objModel.CodigoGuid);


                info.SetMoreInfo(newInfo);

                //****************************************************

                Image image = new Image(imageData);
                image.SetWidth(pdfDocument.GetDefaultPageSize().GetWidth() - 50);
                image.SetAutoScaleHeight(true);

                document.Add(image);
                pdfDocument.Close();

                oRespuesta.Success = true;
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
