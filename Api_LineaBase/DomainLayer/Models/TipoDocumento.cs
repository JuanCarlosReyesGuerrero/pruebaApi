namespace DomainLayer.Models
{
    public class TipoDocumento : BaseEntity
    {        
        public string? Nombre { get; set; }

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
