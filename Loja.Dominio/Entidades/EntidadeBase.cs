using Flunt.Notifications;

namespace Loja.Dominio.Entidades
{
    public abstract class EntidadeBase : Notifiable<Notification>
    {
        public abstract void Validar();
    }
}
