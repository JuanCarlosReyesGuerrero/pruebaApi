namespace DomainLayer.Dtos
{
    public class UsuarioLoginDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Rol { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
