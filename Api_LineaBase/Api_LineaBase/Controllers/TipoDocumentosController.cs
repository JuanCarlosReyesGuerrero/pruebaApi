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
    public class TipoDocumentosController : ControllerBase
    {
        private readonly ICreateLogger _createLogger;
        private readonly ITipoDocumentoService _tipoDocumentoService;
        private readonly IMapper _mapper;

        public TipoDocumentosController(ITipoDocumentoService tipoDocumentoService, IMapper mapper, ICreateLogger createLogger)
        {
            _tipoDocumentoService = tipoDocumentoService;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        [HttpGet(nameof(GetAllTipoDocumentos))]
        public Result GetAllTipoDocumentos()
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _tipoDocumentoService.GetAll().Result.Data);
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpGet(nameof(GetTipoDocumentoById))]
        public Result GetTipoDocumentoById(int Id)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _tipoDocumentoService.GetById(Id).Result.Data);
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPost(nameof(CreateTipoDocumento))]
        public Result CreateTipoDocumento(TipoDocumentoDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _tipoDocumentoService.Insert(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpPost(nameof(UpdateTipoDocumento))]
        public Result UpdateTipoDocumento(TipoDocumentoDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _tipoDocumentoService.Update(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }

        [HttpDelete(nameof(DeleteTipoDocumento))]
        public Result DeleteTipoDocumento(TipoDocumentoDto objModel)
        {
            try
            {
                return Result.CreateMessage(true, string.Empty, _tipoDocumentoService.Delete(objModel));
            }
            catch (Exception ex)
            {
                return Result.CreateMessage(false, ex.Message, null);
            }
        }
    }
}
