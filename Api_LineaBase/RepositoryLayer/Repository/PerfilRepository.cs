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
    public class PerfilRepository : IPerfilRepository
    {
        private readonly ApplicationDbContext _objContext;

        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public PerfilRepository(ApplicationDbContext objContext, IMapper mapper, ICreateLogger createLogger)
        {
            _objContext = objContext;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        public async Task<bool> Delete(PerfilDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Anulado;
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<Perfil>(objModel);

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

        public async Task<List<PerfilDto>> GetAll()
        {
            try
            {
                List<Perfil> listResult = new List<Perfil>();
                List<PerfilDto> lstTemp = new List<PerfilDto>();

                listResult = await _objContext.Perfiles.Where(x => x.Estado != 2).ToListAsync();

                if (listResult.Count > 0)
                {
                    lstTemp = _mapper.Map<List<PerfilDto>>(listResult);
                }

                return lstTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PerfilDto> GetById(int Id)
        {
            Perfil objResult = new Perfil();
            PerfilDto objTemp = new PerfilDto();

            try
            {
                objResult = await _objContext.Perfiles.Where(x => x.Id == Id && x.Estado != 2).FirstAsync();

                if (objResult != null)
                {
                    objTemp = _mapper.Map<PerfilDto>(objResult);
                }

                return objTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Insert(PerfilDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Activo;
                objModel.FechaCreacion = DateTime.UtcNow;

                var objTemp = _mapper.Map<Perfil>(objModel);

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

        public async Task<bool> Update(PerfilDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<Perfil>(objModel);

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