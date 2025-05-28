namespace DomainLayer.Dtos
{
    public class TipoDocumentoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
