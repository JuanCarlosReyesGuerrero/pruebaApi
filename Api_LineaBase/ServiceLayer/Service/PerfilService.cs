using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class PerfilService : IPerfilService
    {        
        private readonly IPerfilRepository _perfilRepository;
        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public PerfilService(IPerfilRepository perfilRepository, ICreateLogger createLogger, IMapper mapper)
        {
            _perfilRepository = perfilRepository;
            _createLogger = createLogger;
            _mapper = mapper;
        }

        public async Task<Result> Delete(PerfilDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _perfilRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _perfilRepository.Update(entity);
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
            result.Data = await _perfilRepository.GetAll();

            return result;
        }

        public async Task<Result> GetById(int Id)
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _perfilRepository.GetById(Id);

            return result;
        }

        public async Task<Result> Insert(PerfilDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _perfilRepository.Insert(entity);

            return result;
        }

        public async Task<Result> Update(PerfilDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _perfilRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _perfilRepository.Update(entity);
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