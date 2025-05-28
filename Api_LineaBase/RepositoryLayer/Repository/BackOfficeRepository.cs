using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.IRepository;
using static Commun.Enums;

namespace RepositoryLayer.Repository
{
    public class BackOfficeRepository : IBackOfficeRepository
    {
        private readonly ApplicationDbContext _objContext;

        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public BackOfficeRepository(ApplicationDbContext objContext, IMapper mapper, ICreateLogger createLogger)
        {
            _objContext = objContext;
            _mapper = mapper;
            _createLogger = createLogger;
        }

        public async Task<UsuarioDto> ValidateLogin(LoginModel objModel)
        {
            Usuario objResult = new Usuario();
            UsuarioDto objTemp = new UsuarioDto();

            try
            {
                objResult = await _objContext.Usuarios.Where(x => x.Login == objModel.UserName).FirstAsync();

                if (objResult != null)
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

        public async Task<bool> UpdateAttempts(LoginModel objModel)
        {
            bool vRespuesta = false;

            try
            {
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

        public async Task<bool> BlockUser(LoginModel objModel)
        {
            bool vRespuesta = false;

            try
            {
                var lstTemp = _mapper.Map<Usuario>(objModel);

                lstTemp.Estado = (int)Enums.Estado.Inactivo;
                lstTemp.EstadoClave = (int?)Enums.Estado.Inactivo;

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

        public async Task<bool> ChangePassword(ChangePasswordModel objModel)
        {
            bool vRespuesta = false;

            try
            {
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

        public async Task<UsuarioDto> GetUsuarioByPassword(ChangePasswordModel objModel)
        {
            Usuario objResult = new Usuario();
            UsuarioDto objTemp = new UsuarioDto();

            try
            {
                objResult = await _objContext.Usuarios.Where(x => x.Login == objModel.UserName && x.Password == objModel.CurrentPassword).FirstAsync();

                if (objResult != null)
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

        public async Task<bool> Update(ChangePasswordModel objModel)
        {
            bool vRespuesta = false;

            try
            {
                var lstTemp = _mapper.Map<Usuario>(objModel);

                lstTemp.Password = objModel.NewPassword;
                lstTemp.EstadoClave = Convert.ToInt32(EstadoClave.CambioUsuario);
                lstTemp.FechaModificacion = DateTime.UtcNow;

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
