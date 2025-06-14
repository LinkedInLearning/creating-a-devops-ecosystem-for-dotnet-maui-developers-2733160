
using Microsoft.Azure.NotificationHubs;
using QRTracker.Interfaces;

namespace QRTracker.Helpers;
public abstract class NotificationRegistrationServiceBase : INotificationRegistrationService
{
    protected readonly IConnectivity _connectivity;
    protected readonly INotificationHubClient _notificationHubClient;
    protected string? _deviceId;

    public NotificationRegistrationServiceBase(IConnectivity connectivity, INotificationHubClient notificationHubClient)
    {
        _connectivity = connectivity;
        _notificationHubClient = notificationHubClient;
    }

    protected abstract Task<Installation?> GetHubInstallation(RegisterDeviceMessage message);

    public Task<bool> RegisterDeviceWithNotificationHub(RegisterDeviceMessage message)
    {
        throw new NotImplementedException();
    }
}
