namespace DomainLayer.Models
{
    public class Perfil : BaseEntity
    {        
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }     

        public virtual ICollection<Permiso>? Permisos { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}