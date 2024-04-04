using Desafio_BackEnd.Domain.Core.Notifications;

namespace Desafio_BackEnd.Domain.Core.Commands
{
    public abstract class Command : Notifiable
    {
        public abstract bool IsValid();
    }
}