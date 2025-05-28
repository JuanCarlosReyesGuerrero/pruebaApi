using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class ModuloService : IModuloService
    {
        private readonly IModuloRepository _moduloRepository;
        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public ModuloService(IModuloRepository moduloRepository, ICreateLogger createLogger, IMapper mapper)
        {
            _moduloRepository = moduloRepository;
            _createLogger = createLogger;
            _mapper = mapper;
        }

        public async Task<Result> Delete(ModuloDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _moduloRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _moduloRepository.Update(entity);
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
            result.Data = await _moduloRepository.GetAll();

            return result;
        }

        public async Task<Result> GetById(int Id)
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _moduloRepository.GetById(Id);

            return result;
        }

        public async Task<Result> Insert(ModuloDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _moduloRepository.Insert(entity);

            return result;
        }

        public async Task<Result> Update(ModuloDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _moduloRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _moduloRepository.Update(entity);
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