using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace Api_Empopasto.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BackOfficeController : ControllerBase
    {
        private readonly ICreateLogger createLogger;
        private readonly IBackOfficeService objService;
        private readonly IMapper mapper;

        public BackOfficeController(IBackOfficeService _objService, IMapper _mapper, ICreateLogger _createLogger)
        {
            this.objService = _objService;
            this.mapper = _mapper;
            this.createLogger = _createLogger;
        }
                
        [HttpPost]
        [Route("InicioSesion")]
        public async Task<IActionResult> InicioSesionAsync([FromBody] LoginModel loginModel)
        {
            Result oRespuesta = new();

            try
            {
                var vRespuesta = await objService.ValidateLogin(loginModel);

                if (vRespuesta.Success)
                {
                    oRespuesta.Success = true;
                    oRespuesta.MessageHttp = Constantes.msjLoginCorrecto;
                    oRespuesta.Data = vRespuesta.Data;

                    return Ok(oRespuesta);
                }
                else
                {
                    oRespuesta.Success = false;
                    oRespuesta.MessageHttp = vRespuesta.MessageHttp;

                    return Ok(oRespuesta);
                }
            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);
                oRespuesta.MessageHttp = ex.Message;

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CambioPassword")]
        public async Task<IActionResult> CambioPasswordAsync([FromBody] ChangePasswordModel loginModel)
        {
            Result oRespuesta = new();

            try
            {
                var vRespuesta = await objService.ChangePassword(loginModel);

                if (vRespuesta.Success)
                {
                    oRespuesta.Success = true;
                    oRespuesta.MessageHttp = vRespuesta.MessageHttp;
                    oRespuesta.Data = vRespuesta.Data;

                    return Ok(oRespuesta);
                }
                else
                {
                    oRespuesta.Success = false;
                    oRespuesta.MessageHttp = vRespuesta.MessageHttp;

                    return Ok(oRespuesta);
                }
            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);
                oRespuesta.MessageHttp = ex.Message;

                return BadRequest();
            }
        }
    }
}
