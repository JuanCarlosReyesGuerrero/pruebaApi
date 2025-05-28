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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _objContext;

        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public UsuarioRepository(ApplicationDbContext objContext, IMapper mapper, ICreateLogger createLogger)
        {
            _objContext = objContext;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        public async Task<bool> Delete(UsuarioDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Estado = (int)Enums.Estado.Anulado;
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<Usuario>(objModel);

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

        public async Task<List<UsuarioDto>> GetAll()
        {
            try
            {
                List<Usuario> listResult = new List<Usuario>();
                List<UsuarioDto> lstTemp = new List<UsuarioDto>();

                listResult = await (from s in _objContext.Usuarios
                                    where s.Estado != 2
                                    orderby s.Id ascending
                                    select new Usuario
                                    {
                                        Id = s.Id,
                                        Codigo = s.Codigo,
                                        NumeroDocumento = s.NumeroDocumento,
                                        Login = s.Login,
                                        NombreApellido = s.NombreApellido,
                                        EstadoClave = s.EstadoClave,
                                        Intentos = s.Intentos,
                                        TipoDocumentoId = s.TipoDocumentoId,
                                        PerfilId = s.PerfilId,
                                        EmpresaId = s.EmpresaId,
                                        Estado = s.Estado,
                                        FechaCreacion = s.FechaCreacion,
                                        FechaModificacion = s.FechaModificacion
                                    }).ToListAsync();

                if (listResult.Count > 0)
                {
                    lstTemp = _mapper.Map<List<UsuarioDto>>(listResult);
                }

                return lstTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioDto> GetById(int id)
        {
            Usuario objResult = new Usuario();
            UsuarioDto objTemp = new UsuarioDto();

            try
            {
                objResult = await (from s in _objContext.Usuarios
                                   where s.Id == id && s.Estado != 2
                                   select new Usuario
                                   {
                                       Id = s.Id,
                                       Codigo = s.Codigo,
                                       NumeroDocumento = s.NumeroDocumento,
                                       Login = s.Login,
                                       NombreApellido = s.NombreApellido,
                                       EstadoClave = s.EstadoClave,
                                       Intentos = s.Intentos,
                                       TipoDocumentoId = s.TipoDocumentoId,
                                       PerfilId = s.PerfilId,
                                       EmpresaId = s.EmpresaId,
                                       Estado = s.Estado,
                                       FechaCreacion = s.FechaCreacion,
                                       FechaModificacion = s.FechaModificacion
                                   }).FirstAsync();

                if (objResult.Id > 0)
                {
                    objTemp = _mapper.Map<UsuarioDto>(objResult);
                }

                return objTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }           
        }

        public async Task<UsuarioDto> GetByIdEliminado(string vNumeroDocumento)
        {
            Usuario objResult = new Usuario();
            UsuarioDto objTemp = new UsuarioDto();

            try
            {
                objResult = await (from s in _objContext.Usuarios
                                where s.NumeroDocumento == vNumeroDocumento && s.Estado == 2
                                select new Usuario
                                {
                                    Id = s.Id,
                                    Codigo = s.Codigo,
                                    NumeroDocumento = s.NumeroDocumento,
                                    Login = s.Login,
                                    NombreApellido = s.NombreApellido,
                                    EstadoClave = s.EstadoClave,
                                    Intentos = s.Intentos,
                                    TipoDocumentoId = s.TipoDocumentoId,
                                    PerfilId = s.PerfilId,
                                    EmpresaId = s.EmpresaId,
                                    Estado = s.Estado,
                                    FechaCreacion = s.FechaCreacion,
                                    FechaModificacion = s.FechaModificacion
                                }).FirstAsync();

                if (objResult.Id > 0)
                {
                    objTemp = _mapper.Map<UsuarioDto>(objResult);
                }

                return objTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioDto> GetByIdPassword(int id)
        {
            Usuario objResult = new Usuario();
            UsuarioDto objTemp = new UsuarioDto();

            try
            {
                objResult = await (from s in _objContext.Usuarios
                                where s.Id == id && s.Estado != 2
                                select new Usuario
                                {
                                    Id = s.Id,
                                    Codigo = s.Codigo,
                                    NumeroDocumento = s.NumeroDocumento,
                                    Login = s.Login,
                                    NombreApellido = s.NombreApellido,
                                    EstadoClave = s.EstadoClave,
                                    Intentos = s.Intentos,
                                    TipoDocumentoId = s.TipoDocumentoId,
                                    PerfilId = s.PerfilId,
                                    EmpresaId = s.EmpresaId,
                                    Estado = s.Estado,
                                    FechaCreacion = s.FechaCreacion,
                                    FechaModificacion = s.FechaModificacion,
                                    Password = s.Password
                                }).FirstAsync();

                if (objResult.Id > 0)
                {
                    objTemp = _mapper.Map<UsuarioDto>(objResult);
                }

                return objTemp;
            }
            catch (Exception ex)
            {
                _createLogger.LogWriteExcepcion(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Insert(UsuarioDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.Login = objModel.Login.ToUpper();
                objModel.Estado = (int)Enums.Estado.Activo;
                objModel.FechaCreacion = DateTime.UtcNow;

                var objTemp = _mapper.Map<Usuario>(objModel);

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

        public async Task<bool> Update(UsuarioDto objModel)
        {
            bool vRespuesta = false;

            try
            {
                objModel.FechaModificacion = DateTime.UtcNow;

                var lstTemp = _mapper.Map<Usuario>(objModel);

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