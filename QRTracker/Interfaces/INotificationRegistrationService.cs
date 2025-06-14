
using QRTracker.Helpers;

namespace QRTracker.Interfaces;
public interface INotificationRegistrationService
{
    Task<bool> RegisterDeviceWithNotificationHub(RegisterDeviceMessage message);
}
