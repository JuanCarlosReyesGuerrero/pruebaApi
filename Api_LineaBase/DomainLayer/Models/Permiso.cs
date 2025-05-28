namespace DomainLayer.Models
{
    public class Permiso : BaseEntity
    {
        public bool Consultar { get; set; }
        public bool Crear { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public int PerfilId { get; set; }
        public int ModuloId { get; set; }

        public virtual Perfil? Perfil { get; set; }
        public virtual Modulo? Modulo { get; set; }
    }
}
