using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Dominio
{
    public class Resultado
    {
        IReadOnlyCollection<Notification> Errors;

        public Resultado()
        {
            Errors = new List<Notification>();
        }

        public void AddErrors(IReadOnlyCollection<Notification> notifications)
        {
            Errors = notifications;
        }

        public IReadOnlyCollection<Notification> GetErrors()
        {
            return Errors;
        }

        public bool HasErrors()
        {
            return Errors.Count > 0;
        }
    }
}
