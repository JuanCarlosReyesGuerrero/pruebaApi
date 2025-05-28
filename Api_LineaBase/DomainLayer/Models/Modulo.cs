namespace DomainLayer.Models
{
    public class Modulo : BaseEntity
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Permiso>? Permisos { get; set; }
    }
}
