using Invio.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Interfaces
{
    public interface INotificationService
    {
        List<Notification> GetNotifications();
        bool HasNotification();
        bool AddNotification(Notification notification);
        bool AddNotification(string key, string message);
    }
}
