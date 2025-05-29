using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _objContext;
        private readonly IMapper _mapper;
        private readonly ICreateLogger _createLogger;

        public ProductoRepository(ApplicationDbContext objContext, IMapper mapper, ICreateLogger createLogger)
        {
            _objContext = objContext;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        public async Task<List<ProductoDto>> GetAllAsync()
        {
            try
            {
                var entities = await _objContext.Productos
                                     .Where(p => p.Estado != (int)Enums.Estado.Anulado)
                                     .OrderBy(p => p.Id)
                                     .ToListAsync();
                return _mapper.Map<List<ProductoDto>>(entities);
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw; // Re-throw the original exception to be handled by a global handler or calling layer
            }
        }

        public async Task<ProductoDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _objContext.Productos
                                     .FirstOrDefaultAsync(p => p.Id == id && p.Estado != (int)Enums.Estado.Anulado);
                return _mapper.Map<ProductoDto>(entity);
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw;
            }
        }

        public async Task<ProductoDto> InsertAsync(ProductoDto productoDto)
        {
            try
            {
                var entity = _mapper.Map<Producto>(productoDto);
                entity.Estado = (int)Enums.Estado.Activo;
                entity.FechaCreacion = DateTime.UtcNow;

                await _objContext.Productos.AddAsync(entity);
                await _objContext.SaveChangesAsync();

                return _mapper.Map<ProductoDto>(entity);
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw;
            }
        }

        public async Task<ProductoDto> UpdateAsync(ProductoDto productoDto)
        {
            try
            {
                var entity = _mapper.Map<Producto>(productoDto);
                entity.FechaModificacion = DateTime.UtcNow;
                // Ensure the ID is correctly set for updating the existing entity
                // If productoDto.Id is not the PK, ensure the correct PK is used.
                // For BaseEntity, Id is the PK.

                _objContext.Productos.Update(entity);
                // If you want to be more explicit about tracking:
                // _objContext.Entry(entity).State = EntityState.Modified;
                await _objContext.SaveChangesAsync();

                return _mapper.Map<ProductoDto>(entity);
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _objContext.Productos.FindAsync(id);
                if (entity == null || entity.Estado == (int)Enums.Estado.Anulado)
                {
                    return false; // Or throw a NotFoundException if that's the project's pattern
                }

                entity.Estado = (int)Enums.Estado.Anulado;
                entity.FechaModificacion = DateTime.UtcNow;

                _objContext.Productos.Update(entity);
                // _objContext.Entry(entity).State = EntityState.Modified; // Alternative
                await _objContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                // Depending on the desired behavior, you might re-throw or return false.
                // For now, returning false as per the plan for catch blocks in Delete.
                return false;
            }
        }
    }
}
