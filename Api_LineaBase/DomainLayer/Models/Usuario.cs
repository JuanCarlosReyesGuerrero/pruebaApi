namespace DomainLayer.Models
{
    public class Usuario : BaseEntity
    {
        public string? Codigo { get; set; }        
        public string? NumeroDocumento { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? NombreApellido { get; set; }        
        public int? EstadoClave { get; set; }
        public int? Intentos { get; set; }
        public int? TipoDocumentoId { get; set; }
        public int? PerfilId { get; set; }
        public int? EmpresaId { get; set; }

        public virtual TipoDocumento? TipoDocumento { get; set; }
        public virtual Perfil? Perfil { get; set; }        
    }
}
