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
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly ApplicationDbContext _objContext;

        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public TipoDocumentoRepository(ApplicationDbContext objContext, IMapper mapper, ICreateLogger createLogger)
        {
            _objContext = objContext;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        public async Task<bool> Delete(TipoDocumentoDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Anulado;
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<TipoDocumento>(objModel);

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

        public async Task<List<TipoDocumentoDto>> GetAll()
        {
            try
            {
                List<TipoDocumento> listResult = new List<TipoDocumento>();
                List<TipoDocumentoDto> lstTemp = new List<TipoDocumentoDto>();

                listResult = await _objContext.TipoDocumentos.Where(x => x.Estado != 2).ToListAsync();

                if (listResult.Count > 0)
                {
                    lstTemp = _mapper.Map<List<TipoDocumentoDto>>(listResult);
                }

                return lstTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<TipoDocumentoDto> GetById(int Id)
        {
            TipoDocumento objResult = new TipoDocumento();
            TipoDocumentoDto objTemp = new TipoDocumentoDto();

            try
            {
                objResult = await _objContext.TipoDocumentos.Where(x => x.Id == Id && x.Estado != 2).FirstAsync();

                if (objResult != null)
                {
                    objTemp = _mapper.Map<TipoDocumentoDto>(objResult);
                }

                return objTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Insert(TipoDocumentoDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Activo;
                objModel.FechaCreacion = DateTime.UtcNow;

                var objTemp = _mapper.Map<TipoDocumento>(objModel);

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

        public async Task<bool> Update(TipoDocumentoDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<TipoDocumento>(objModel);

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