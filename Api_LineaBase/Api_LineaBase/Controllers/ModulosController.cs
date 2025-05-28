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
    public class ModulosController : ControllerBase
    {
        private readonly IModuloService _moduloService;
        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public ModulosController(IModuloService moduloService, IMapper mapper, ICreateLogger createLogger)
        {
            _moduloService = moduloService;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        [HttpGet(nameof(GetModuloById))]
        public Result GetModuloById(int Id)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _moduloService.GetById(Id).Result.Data);
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpGet(nameof(GetAllModulos))]
        public Result GetAllModulos()
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _moduloService.GetAll().Result.Data);
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPost(nameof(CreateModulo))]
        public Result CreateModulo(ModuloDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _moduloService.Insert(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPut(nameof(UpdateModulo))]
        public Result UpdateModulo(ModuloDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _moduloService.Update(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpDelete(nameof(DeleteModulo))]
        public Result DeleteModulo(ModuloDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _moduloService.Delete(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }
    }
}
