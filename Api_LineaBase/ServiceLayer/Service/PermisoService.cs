using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class PermisoService : IPermisoService
    {
        private readonly ICreateLogger _createLogger;
        private readonly IPermisoRepository _objPermisoRepository;
        private readonly IPerfilRepository _objPerfilRepository;
        private readonly IModuloRepository _objModuloRepository;
        private readonly IUsuarioRepository _objUsuarioRepository;

        public PermisoService(
            IPermisoRepository objPermisoRepository, IPerfilRepository objPerfilRepository,
            IModuloRepository objModuloRepository, IUsuarioRepository objUsuarioRepository, ICreateLogger createLogger)
        {
            _objPermisoRepository = objPermisoRepository;
            _objPerfilRepository = objPerfilRepository;
            _objModuloRepository = objModuloRepository;
            _objUsuarioRepository = objUsuarioRepository;
            _createLogger = createLogger;
        }

        public async Task<Result> Delete(PermisoDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _objPermisoRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _objPermisoRepository.Update(entity);
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
            result.Data = await _objPermisoRepository.GetAll();

            return result;
        }

        public async Task<Result> GetById(int Id)
        {
            Result result = new Result();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _objPermisoRepository.GetById(Id);

            return result;
        }

        public async Task<Result> Insert(PermisoDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = await _objPermisoRepository.Insert(entity);

            return result;
        }

        public async Task<Result> Update(PermisoDto entity)
        {
            Result result = new Result();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _objPermisoRepository.GetById(entity.Id);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _objPermisoRepository.Update(entity);
            }
            else
            {
                result.Success = false;
                result.MessageHttp = Constantes.msjRegNoActualizado;
                result.Data = false;
            }

            return result;
        }

        public async Task<Result> GetAllFull()
        {
            Result result = new Result();

            List<PermisoDto> vObjPermiso = new List<PermisoDto>();
            List<PerfilDto> vObjPerfil = new List<PerfilDto>();
            List<ModuloDto> vObjModulo = new List<ModuloDto>();


            vObjPermiso = await _objPermisoRepository.GetAll();
            vObjPerfil = await _objPerfilRepository.GetAll();
            vObjModulo = await _objModuloRepository.GetAll();

            var vObjResult = (from permiso in vObjPermiso
                              join perfil in vObjPerfil on permiso.PerfilId equals perfil.Id
                              join modulo in vObjModulo on permiso.ModuloId equals modulo.Id
                              select new PermisoFullDto()
                              {
                                  Id = permiso.Id,
                                  PerfilId = permiso.PerfilId,
                                  ModuloId = permiso.ModuloId,
                                  Consultar = permiso.Consultar,
                                  Crear = permiso.Crear,
                                  Editar = permiso.Editar,
                                  Eliminar = permiso.Eliminar,
                                  Estado = permiso.Estado,
                                  FechaCreacion = permiso.FechaCreacion,
                                  FechaModificacion = permiso.FechaModificacion,

                                  Perfiles = perfil,
                                  Modulos = modulo
                              }).ToList();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = vObjResult;

            return result;
        }

        public async Task<Result> GetAllFullByIdUsuario(int Id)
        {
            Result result = new Result();

            List<PermisoDto> vObjPermiso = new List<PermisoDto>();
            List<PerfilDto> vObjPerfil = new List<PerfilDto>();
            List<ModuloDto> vObjModulo = new List<ModuloDto>();
            List<UsuarioDto> vObjUsuario = new List<UsuarioDto>();

            vObjPermiso = await _objPermisoRepository.GetAll();
            vObjPerfil = await _objPerfilRepository.GetAll();
            vObjModulo = await _objModuloRepository.GetAll();
            vObjModulo = await _objModuloRepository.GetAll();
            vObjUsuario = await _objUsuarioRepository.GetAll();

            var vObjResult = (from vPermiso in vObjPermiso
                              join vPerfil in vObjPerfil on vPermiso.PerfilId equals vPerfil.Id
                              join vModulo in vObjModulo on vPermiso.ModuloId equals vModulo.Id
                              join vUsuario in vObjUsuario on vPerfil.Id equals vUsuario.PerfilId
                              where vUsuario.Id == Id
                              select new PermisoFullDto()
                              {
                                  Id = vPermiso.Id,
                                  PerfilId = vPermiso.PerfilId,
                                  ModuloId = vPermiso.ModuloId,
                                  Consultar = vPermiso.Consultar,
                                  Crear = vPermiso.Crear,
                                  Editar = vPermiso.Editar,
                                  Eliminar = vPermiso.Eliminar,
                                  Estado = vPermiso.Estado,
                                  FechaCreacion = vPermiso.FechaCreacion,
                                  FechaModificacion = vPermiso.FechaModificacion,

                                  Perfiles = vPerfil,
                                  Modulos = vModulo
                              }).ToList();

            result.Success = true;
            result.MessageHttp = Constantes.msjMs200;
            result.Data = vObjResult;

            return result;
        }
    }
}