
using Microsoft.Azure.NotificationHubs;
using QRTracker.Helpers;
using UIKit;

namespace QRTracker.Platforms.iOS;
public class NotificationRegistrationService : NotificationRegistrationServiceBase
{
    public NotificationRegistrationService(IConnectivity connectivity, INotificationHubClient notificationHubClient) : base(connectivity, notificationHubClient)
    {
        _deviceId = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
    }

    protected override Task<Installation?> GetHubInstallation(RegisterDeviceMessage message)
    {
        var tcs = new TaskCompletionSource<Installation?>();

        var installation = new Installation
        {
            InstallationId = _deviceId,
            Platform = NotificationPlatform.Apns,
            PushChannel = message.Token
        };
        tcs.SetResult(installation);

        return tcs.Task;
    }
}
