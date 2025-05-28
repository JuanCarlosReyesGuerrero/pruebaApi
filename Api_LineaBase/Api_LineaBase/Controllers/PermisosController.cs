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
    public class PermisosController : ControllerBase
    {
        private readonly ICreateLogger createLogger;
        private readonly IPermisoService _objService;
        private readonly IMapper mapper;

        public PermisosController(IPermisoService objService, IMapper _mapper, ICreateLogger _createLogger)
        {
            _objService = objService;
            this.mapper = _mapper;
            this.createLogger = _createLogger;
        }

        [HttpGet(nameof(GetPermisoById))]
        public Result GetPermisoById(int Id)
        {
            Result oRespuesta = new Result();

            try
            {
                var queryTable = _objService.GetById(Id);

                var lstTemp = mapper.Map<List<PermisoDto>>(queryTable.Result.Data);

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

        [HttpGet(nameof(GetAllPermisos))]
        public Result GetAllPermisos()
        {
            Result oRespuesta = new Result();

            try
            {
                var queryTable = _objService.GetAll();

                var lstTemp = mapper.Map<List<PermisoDto>>(queryTable.Result.Data);

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

        [HttpPost(nameof(CreatePermiso))]
        public Result CreatePermiso(PermisoDto objModel)
        {
            Result oRespuesta = new Result();

            try
            {
                var lstTemp = mapper.Map<PermisoDto>(objModel);

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

        [HttpPut(nameof(UpdatePermiso))]
        public Result UpdatePermiso(PermisoDto objModel)
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

        [HttpDelete(nameof(DeletePermiso))]
        public Result DeletePermiso(PermisoDto objModel)
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

        [HttpGet(nameof(GetAllPermisosFull))]
        public Result GetAllPermisosFull()
        {
            Result oRespuesta = new Result();

            try
            {
                var queryTable = _objService.GetAllFull();

                var lstTemp = mapper.Map<List<PermisoFullDto>>(queryTable.Result.Data);

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

        [HttpGet(nameof(GetAllFullByIdUsuario))]
        public Result GetAllFullByIdUsuario(int Id)
        {
            Result oRespuesta = new Result();

            try
            {
                var queryTable = _objService.GetAllFullByIdUsuario(Id);

                var lstTemp = mapper.Map<List<PermisoFullDto>>(queryTable.Result.Data);

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
    }
}
