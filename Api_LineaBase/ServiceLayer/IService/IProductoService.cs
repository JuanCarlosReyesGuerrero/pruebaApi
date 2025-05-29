using DomainLayer.Dtos;
using DomainLayer.Models; // For Result class
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IProductoService
    {
        Task<Result> GetAllProductosAsync();
        Task<Result> GetProductoByIdAsync(int id);
        Task<Result> CreateProductoAsync(ProductoDto productoDto);
        Task<Result> UpdateProductoAsync(ProductoDto productoDto);
        Task<Result> DeleteProductoAsync(int id);
    }
}
