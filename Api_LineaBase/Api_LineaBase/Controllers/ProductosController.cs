using AutoMapper;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;
using System;
using System.Threading.Tasks;

namespace Api_Empopasto.Controllers // Using namespace from UsuarioController.cs
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper; // Included for consistency, may not be used directly
        private readonly ICreateLogger _createLogger;

        public ProductosController(IProductoService productoService, IMapper mapper, ICreateLogger createLogger)
        {
            _productoService = productoService;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        [HttpGet(nameof(GetAllProductos))]
        public async Task<Result> GetAllProductos()
        {
            try
            {
                return await _productoService.GetAllProductosAsync();
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpGet(nameof(GetProductoById))]
        public async Task<Result> GetProductoById(int id) // id as query parameter
        {
            try
            {
                return await _productoService.GetProductoByIdAsync(id);
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPost(nameof(CreateProducto))]
        public async Task<Result> CreateProducto([FromBody] ProductoDto productoDto)
        {
            try
            {
                return await _productoService.CreateProductoAsync(productoDto);
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPut(nameof(UpdateProducto))]
        public async Task<Result> UpdateProducto([FromBody] ProductoDto productoDto)
        {
            try
            {
                return await _productoService.UpdateProductoAsync(productoDto);
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpDelete(nameof(DeleteProducto))] // Route will be api/Productos/DeleteProducto?id=X
        public async Task<Result> DeleteProducto(int id) // id as query parameter
        {
            try
            {
                return await _productoService.DeleteProductoAsync(id);
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                return Result.CreateMessage(false, ex.Message, null);
            }
        }
    }
}
