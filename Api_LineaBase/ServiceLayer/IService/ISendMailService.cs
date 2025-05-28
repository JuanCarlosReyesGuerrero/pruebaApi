using DomainLayer.Models;

namespace ServiceLayer.IService
{
    public interface ISendMailService
    {
        Task<bool> EnviarMail(Persona persona, MensajeModel mensaje);
    }
}
