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
    public class ModuloRepository : IModuloRepository
    {
        private readonly ApplicationDbContext _objContext;

        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public ModuloRepository(ApplicationDbContext objContext, IMapper mapper, ICreateLogger createLogger)
        {
            _objContext = objContext;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        public async Task<bool> Delete(ModuloDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Anulado;
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<Modulo>(objModel);

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

        public async Task<List<ModuloDto>> GetAll()
        {
            try
            {
                List<Modulo> listResult = new List<Modulo>();
                List<ModuloDto> lstTemp = new List<ModuloDto>();

                listResult = await _objContext.Modulos.Where(x => x.Estado != 2).ToListAsync();

                if (listResult.Count > 0)
                {
                    lstTemp = _mapper.Map<List<ModuloDto>>(listResult);
                }

                return lstTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModuloDto> GetById(int Id)
        {
            Modulo objResult = new Modulo();
            ModuloDto objTemp = new ModuloDto();

            try
            {
                objResult = await _objContext.Modulos.Where(x => x.Id == Id && x.Estado != 2).FirstAsync();

                if (objResult != null)
                {
                    objTemp = _mapper.Map<ModuloDto>(objResult);
                }

                return objTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Insert(ModuloDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Activo;
                objModel.FechaCreacion = DateTime.UtcNow;

                var objTemp = _mapper.Map<Modulo>(objModel);

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

        public async Task<bool> Update(ModuloDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<Modulo>(objModel);

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