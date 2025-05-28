using AutoMapper;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace Api_Empopasto.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ICreateLogger _createLogger;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper, ICreateLogger createLogger)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        [HttpGet(nameof(GetUsuarioById))]
        public Result GetUsuarioById(int Id)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _usuarioService.GetById(Id).Result.Data);
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpGet(nameof(GetAllUsuarios))]
        public Result GetAllUsuarios()
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _usuarioService.GetAll().Result.Data);
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPost(nameof(CreateUsuario))]
        public Result CreateUsuario(UsuarioDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _usuarioService.Insert(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPut(nameof(UpdateUsuario))]
        public Result UpdateUsuario(UsuarioDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _usuarioService.Update(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpDelete(nameof(DeleteUsuario))]
        public Result DeleteUsuario(UsuarioDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _usuarioService.Delete(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPut(nameof(UpdateUsuarioEliminado))]
        public Result UpdateUsuarioEliminado(UsuarioDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _usuarioService.UpdateEliminado(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }
    }
}
