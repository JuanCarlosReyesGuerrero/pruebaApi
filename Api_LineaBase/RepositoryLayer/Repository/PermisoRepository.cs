using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.IRepository;
using System.Data;

namespace RepositoryLayer.Repository
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly ApplicationDbContext _objContext;

        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public PermisoRepository(ApplicationDbContext objContext, IMapper mapper, ICreateLogger createLogger)
        {
            _objContext = objContext;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        public async Task<bool> Delete(PermisoDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Anulado;
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<Permiso>(objModel);

                _objContext.Update(lstTemp);

                await _objContext.SaveChangesAsync();

                vRespuesta = true;

                return vRespuesta;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PermisoDto>> GetAll()
        {
            try
            {
                List<Permiso> listResult = new List<Permiso>();
                List<PermisoDto> lstTemp = new List<PermisoDto>();

                listResult = await _objContext.Permisos.Where(x => x.Estado != 2).ToListAsync();

                if (listResult.Count > 0)
                {
                    lstTemp = _mapper.Map<List<PermisoDto>>(listResult);
                }

                return lstTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PermisoDto> GetById(int Id)
        {
            Permiso objResult = new Permiso();
            PermisoDto objTemp = new PermisoDto();

            try
            {
                objResult = await _objContext.Permisos.Where(x => x.Id == Id && x.Estado != 2).FirstAsync();

                if (objResult != null)
                {
                    objTemp = _mapper.Map<PermisoDto>(objResult);
                }

                return objTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Insert(PermisoDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Activo;
                objModel.FechaCreacion = DateTime.UtcNow;

                var objTemp = _mapper.Map<Permiso>(objModel);

                await _objContext.AddAsync(objTemp);
                await _objContext.SaveChangesAsync();

                vRespuesta = true;

                return vRespuesta;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(PermisoDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<Permiso>(objModel);

                _objContext.Update(lstTemp);

                await _objContext.SaveChangesAsync();

                vRespuesta = true;

                return vRespuesta;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}