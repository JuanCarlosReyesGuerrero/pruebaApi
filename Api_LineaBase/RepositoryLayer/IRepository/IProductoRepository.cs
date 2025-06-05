using DomainLayer.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RepositoryLayer.IRepository
{
    public interface IProductoRepository
    {
        Task<List<ProductoDto>> GetAllAsync();
        Task<ProductoDto> GetByIdAsync(int id);
        Task<ProductoDto> InsertAsync(ProductoDto entity);
        Task<ProductoDto> UpdateAsync(ProductoDto entity);
        Task<bool> DeleteAsync(int id);
    }
}
