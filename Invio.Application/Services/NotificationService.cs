using Invio.Application.Interfaces;
using Invio.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly List<Notification> _notificationList;

        public NotificationService(List<Notification> notificationList)
        {
            _notificationList = new List<Notification>();
        }

        public bool AddNotification(Notification notification)
        {
            _notificationList.Add(notification);

            return false;
        }

        public bool AddNotification(string key, string message)
        {
            var notification = new Notification()
            {
                Key = key,
                Message = message
            };

            _notificationList.Add(notification);

            return false;
        }

        public List<Notification> GetNotifications() =>
        _notificationList;

        public bool HasNotification() =>
        _notificationList.Any();
    }
}
