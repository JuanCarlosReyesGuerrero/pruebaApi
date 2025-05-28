namespace DomainLayer.Dtos
{
    public class PermisoFullDto
    {
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public int ModuloId { get; set; }
        public int? Consultar { get; set; }
        public int? Crear { get; set; }
        public int? Editar { get; set; }
        public int? Eliminar { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public PerfilDto? Perfiles { get; set; }
        public ModuloDto? Modulos { get; set; }
    }
}
