using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class TipoDocumentoService : ITipoDocumentoService
    {       
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;
        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public TipoDocumentoService(ITipoDocumentoRepository tipoDocumentoRepository, 
            ICreateLogger createLogger, IMapper mapper)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
            _createLogger = createLogger;
            _mapper = mapper;
        }

        public async Task<Result> Delete(TipoDocumentoDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _tipoDocumentoRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _tipoDocumentoRepository.Update(entity);
            }
            else
            {
                result.Success = false;
                result.MessageHttp = Constantes.msjRegNoActualizado;
                result.Data = false;
            }

            return result;
        }

        public async Task<Result> GetAll()
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _tipoDocumentoRepository.GetAll();

            return result;
        }

        public async Task<Result> GetById(int Id)
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _tipoDocumentoRepository.GetById(Id);

            return result;
        }

        public async Task<Result> Insert(TipoDocumentoDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _tipoDocumentoRepository.Insert(entity);

            return result;
        }

        public async Task<Result> Update(TipoDocumentoDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _tipoDocumentoRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _tipoDocumentoRepository.Update(entity);
            }
            else
            {
                result.Success = false;
                result.MessageHttp = Constantes.msjRegNoActualizado;
                result.Data = false;
            }

            return result;
        }        
    }
}
