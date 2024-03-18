namespace Desafio_BackEnd.Domain.Core.Notifications
{
    public class Notifiable
    {
        private readonly List<Notificacao> _notifications = [];

        public IReadOnlyCollection<Notificacao> Notifications
        { get { return _notifications; } }

        public bool Invalid
        { get { return Notifications != null && Notifications.Count > 0; } }

        public bool Valid
        { get { return Notifications == null || Notifications.Count == 0; } }

        public void AddNotification(string property, string message, string origem = "APIDesafio-BackEnd")
        {
            _notifications.Add(new Notificacao(property, message, origem));
        }

        public void AddNotifications(ICollection<Notificacao> notifications)
        {
            if (notifications == null) return;
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(Notifiable item)
        {
            if (item == null) return;
            if (item.Notifications != null)
                _notifications.AddRange(item.Notifications);
        }
    }

    public class Notificacao(string property, string message, string origem = "APIDesafio-BackEnd")
    {
        public string Property { get; private set; } = property;
        public string Message { get; private set; } = message;
        public string Origem { get; private set; } = origem;
    }
}