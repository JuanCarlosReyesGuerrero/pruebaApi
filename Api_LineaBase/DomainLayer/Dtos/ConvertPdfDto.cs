namespace DomainLayer.Dtos
{
    public class ConvertPdfDto
    {
        public string? ArchivoOrigen { get; set; }
        public string? ArchivoDestino { get; set; }
        public string? CodigoGuid { get; set; }
        public string? Fechalectura { get; set; }
        public string? Consecutivo { get; set; }       
    }
}
