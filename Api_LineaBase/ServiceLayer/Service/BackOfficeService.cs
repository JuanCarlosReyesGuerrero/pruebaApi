using AutoMapper;
using Commun;
using Commun.Logger;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class BackOfficeService : IBackOfficeService
    {
        private readonly IBackOfficeRepository _backOfficeRepository;
        private readonly ICreateLogger _createLogger;
        private readonly IMapper _mapper;

        public BackOfficeService(IBackOfficeRepository backOfficeRepository, ICreateLogger createLogger, IMapper mapper)
        {
            _backOfficeRepository = backOfficeRepository;
            _createLogger = createLogger;
            _mapper = mapper;
        }

        public async Task<Result> ValidateLogin(LoginModel objModel)
        {
            Result result = new Result();

            int vIntentos = 0;

            var objRespuesta = await _backOfficeRepository.ValidateLogin(objModel);

            if (objRespuesta.Id > 0)
            {
                if (objRespuesta.Estado == Convert.ToInt32(Enums.Estado.Anulado))
                {
                    result.Success = false;
                    result.MessageHttp = Constantes.msjUsuarioEliminado;
                }
                else if (objRespuesta.Intentos >= 5)
                {
                    await _backOfficeRepository.BlockUser(objModel);

                    result.Success = false;
                    result.MessageHttp = Constantes.msjUsuarioBloqueado;
                }
                else if (objRespuesta.Password != objModel.Password)
                {
                    var vIntentosTemp = await _backOfficeRepository.UpdateAttempts(objModel);

                    vIntentos++;

                    result.Success = false;

                    if (vIntentos == 4)
                        result.MessageHttp = "Las credenciales son incorrectas, te queda 1 intento";
                    else
                        result.MessageHttp = "Las credenciales son incorrectas";
                }
                else
                {

                    await _backOfficeRepository.UpdateAttempts(objModel);

                    vIntentos = 0;

                    result.Success = true;
                    result.Data = objRespuesta;
                }
            }
            else
            {
                result.Success = false;
                result.MessageHttp = "Las credenciales son incorrectas";
                result.Data = null;
            }

            return result;
        }

        public async Task<Result> ChangePassword(ChangePasswordModel objModel)
        {
            Result result = new Result();

            if (objModel == null)
            {
                throw new ArgumentNullException("entity");
            }

            var objExiste = await _backOfficeRepository.GetUsuarioByPassword(objModel);

            if (objExiste != null)
            {
                result.Success = true;
                result.MessageHttp = Constantes.msjRegActualizado;
                result.Data = await _backOfficeRepository.Update(objModel);
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
