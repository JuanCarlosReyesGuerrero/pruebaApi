using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.IService;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ICreateLogger _createLogger;

        public ProductoService(IProductoRepository productoRepository, IMapper mapper, ICreateLogger createLogger)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        public async Task<Result> GetAllProductosAsync()
        {
            Result result = new Result();
            try
            {
                var productos = await _productoRepository.GetAllAsync();
                result.Data = productos;
                result.Success = true;
                result.MessageHttp = Constantes.msjMs200;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                result.Success = false;
                result.MessageHttp = Constantes.msjErrorInterno;
                result.Data = ex.Message; // Or null
            }
            return result;
        }

        public async Task<Result> GetProductoByIdAsync(int id)
        {
            Result result = new Result();
            try
            {
                var producto = await _productoRepository.GetByIdAsync(id);
                if (producto != null)
                {
                    result.Data = producto;
                    result.Success = true;
                    result.MessageHttp = Constantes.msjMs200;
                }
                else
                {
                    result.Success = false;
                    result.MessageHttp = Constantes.msjNoEncontrado; // "Producto no encontrado"
                    result.Data = null;
                }
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                result.Success = false;
                result.MessageHttp = Constantes.msjErrorInterno;
                result.Data = ex.Message; // Or null
            }
            return result;
        }

        public async Task<Result> CreateProductoAsync(ProductoDto productoDto)
        {
            Result result = new Result();
            try
            {
                if (productoDto == null)
                {
                    result.Success = false;
                    result.MessageHttp = Constantes.msjRequiereObjeto; // "Request object cannot be null"
                    result.Data = null;
                    return result;
                }

                // Add any other specific validations for productoDto here if needed
                // Example: if (string.IsNullOrWhiteSpace(productoDto.Nombre)) { ... }

                var nuevoProducto = await _productoRepository.InsertAsync(productoDto);
                result.Data = nuevoProducto;
                result.Success = true;
                result.MessageHttp = Constantes.msjRegGuardado; // "Registro guardado exitosamente"
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                result.Success = false;
                result.MessageHttp = Constantes.msjErrorAlGuardar; // "Error al guardar el registro"
                result.Data = ex.Message; // Or null
            }
            return result;
        }

        public async Task<Result> UpdateProductoAsync(ProductoDto productoDto)
        {
            Result result = new Result();
            try
            {
                if (productoDto == null)
                {
                    result.Success = false;
                    result.MessageHttp = Constantes.msjRequiereObjeto; // "Request object cannot be null"
                    result.Data = null;
                    return result;
                }

                // Optional: Check if product exists before attempting update, if repository doesn't handle it robustly
                var existingProducto = await _productoRepository.GetByIdAsync(productoDto.Id);
                if (existingProducto == null)
                {
                    result.Success = false;
                    result.MessageHttp = Constantes.msjNoEncontrado; // "Producto no encontrado para actualizar"
                    result.Data = null;
                    return result;
                }

                var productoActualizado = await _productoRepository.UpdateAsync(productoDto);
                result.Data = productoActualizado;
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado; // "Registro actualizado exitosamente"
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                result.Success = false;
                result.MessageHttp = Constantes.msjErrorAlActualizar; // "Error al actualizar el registro"
                result.Data = ex.Message; // Or null
            }
            return result;
        }

        public async Task<Result> DeleteProductoAsync(int id)
        {
            Result result = new Result();
            try
            {
                var eliminado = await _productoRepository.DeleteAsync(id);
                if (eliminado)
                {
                    result.Success = true;
                    result.MessageHttp = Constantes.msjRegEliminado; // "Registro eliminado exitosamente"
                    result.Data = true;
                }
                else
                {
                    result.Success = false;
                    // This could be because it was not found, or another reason for deletion failure.
                    result.MessageHttp = Constantes.msjErrorAlEliminar; // "Error al eliminar el registro o registro no encontrado"
                    result.Data = false;
                }
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                result.Success = false;
                result.MessageHttp = Constantes.msjErrorAlEliminar; // "Error al eliminar el registro"
                result.Data = ex.Message; // Or false
            }
            return result;
        }
    }
}
