namespace DomainLayer.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
