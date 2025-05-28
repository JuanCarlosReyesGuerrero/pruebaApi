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
    public class PerfilesController : ControllerBase
    {
        private readonly ICreateLogger createLogger;
        private readonly IPerfilService _objService;
        private readonly IMapper mapper;

        public PerfilesController(IPerfilService objService, IMapper _mapper, ICreateLogger _createLogger)
        {
            _objService = objService;
            this.mapper = _mapper;
            this.createLogger = _createLogger;
        }

        [HttpGet(nameof(GetPerfilById))]
        public Result GetPerfilById(int Id)
        {
            Result oRespuesta = new Result();

            try
            {
                var queryTable = _objService.GetById(Id);

                var lstTemp = mapper.Map<List<PerfilDto>>(queryTable.Result.Data);

                if (lstTemp.Count >= 0)
                {
                    oRespuesta.Success = true;
                    oRespuesta.Data = lstTemp;
                }
            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);

                oRespuesta.MessageHttp = ex.Message;
            }

            return oRespuesta;
        }

        [HttpGet(nameof(GetAllPerfiles))]
        public Result GetAllPerfiles()
        {
            Result oRespuesta = new Result();

            try
            {
                var queryTable = _objService.GetAll();

                var lstTemp = mapper.Map<List<PerfilDto>>(queryTable.Result.Data);

                if (lstTemp.Count >= 0)
                {
                    oRespuesta.Success = true;
                    oRespuesta.Data = lstTemp;
                }
            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);

                oRespuesta.MessageHttp = ex.Message;
            }

            return oRespuesta;
        }

        [HttpPost(nameof(CreatePerfil))]
        public Result CreatePerfil(PerfilDto objModel)
        {
            Result oRespuesta = new Result();

            try
            {
                var lstTemp = mapper.Map<PerfilDto>(objModel);

                var vTemp = _objService.Insert(lstTemp);

                oRespuesta.Success = vTemp.Result.Success;
                oRespuesta.MessageHttp = vTemp.Result.MessageHttp;
            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);

                oRespuesta.MessageHttp = ex.Message;
            }

            return oRespuesta;
        }

        [HttpPut(nameof(UpdatePerfil))]
        public Result UpdatePerfil(PerfilDto objModel)
        {
            Result oRespuesta = new Result();

            try
            {
                var vTemp = _objService.Update(objModel);

                oRespuesta.Success = vTemp.Result.Success;
                oRespuesta.MessageHttp = vTemp.Result.MessageHttp;

            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);

                oRespuesta.MessageHttp = ex.Message;
            }

            return oRespuesta;
        }

        [HttpDelete(nameof(DeletePerfil))]
        public Result DeletePerfil(PerfilDto objModel)
        {
            Result oRespuesta = new Result();

            try
            {
                var vTemp = _objService.Delete(objModel);

                oRespuesta.Success = vTemp.Result.Success;
                oRespuesta.MessageHttp = vTemp.Result.MessageHttp;

            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);

                oRespuesta.MessageHttp = ex.Message;
            }

            return oRespuesta;
        }
    }
}
