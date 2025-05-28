using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _objUsuarioRepository;
        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository objUsuarioRepository,
            ICreateLogger createLogger, IMapper mapper)
        {
            _objUsuarioRepository = objUsuarioRepository;
            _createLogger = createLogger;
            _mapper = mapper;
        }

        public Task<Result> Delete(UsuarioDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> GetAll()
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _objUsuarioRepository.GetAll();

            return result;
        }

        public async Task<Result> GetById(int id)
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _objUsuarioRepository.GetById(id);

            return result;
        }

        public async Task<Result> GetByIdEliminado(string vNumeroDocumento)
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _objUsuarioRepository.GetByIdEliminado(vNumeroDocumento);

            return result;
        }

        public async Task<Result> GetByIdPassword(int id)
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _objUsuarioRepository.GetByIdPassword(id);

            return result;
        }

        public async Task<Result> Insert(UsuarioDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _objUsuarioRepository.Insert(entity);

            return result;
        }

        public async Task<Result> Update(UsuarioDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _objUsuarioRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _objUsuarioRepository.Update(entity);
            }
            else
            {
                result.Success = false;
                result.MessageHttp = Constantes.msjRegNoActualizado;
                result.Data = false;
            }

            return result;
        }

        public async Task<Result> UpdateEliminado(UsuarioDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _objUsuarioRepository.GetByIdEliminado(entity.NumeroDocumento);

            if (objExiste != null)
            {
                if (objExiste.Id > 0)
                {
                    objExiste.Estado = 1;
                    objExiste.Intentos = 0;
                }

                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _objUsuarioRepository.Update(entity);
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
