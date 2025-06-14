using CommunityToolkit.Mvvm.Messaging;
using Foundation;
using QRTracker.Helpers;
using UIKit;
using UserNotifications;

namespace QRTracker;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate, IUNUserNotificationCenterDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var launchResult = base.FinishedLaunching(application, launchOptions);

        var authOptiopns = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
        UNUserNotificationCenter.Current.RequestAuthorization(authOptiopns, (granted, error) => 
        {
            if (granted && error == null)
            {
                this.InvokeOnMainThread(() =>
                {
                    UIApplication.SharedApplication.RegisterForRemoteNotifications();
                    UNUserNotificationCenter.Current.Delegate = this;
                });
            }
        });

        return launchResult;
    }

    [Export("application.didRegisterForRemoteNotificationsWithDeviceToken:")]
    public void RegisteredForRemoteNotificaitonsWithDeviceToken(UIApplication application, NSData deviceToken)
    {
        if (deviceToken.Length > 0)
        {
            string _token = string.Empty;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                var data = deviceToken.ToArray();
                _token = BitConverter.ToString(data)
                    .Replace("-", "")
                    .Replace("\"", "");
            }
            else if (!string.IsNullOrEmpty(deviceToken.Description))
            {
                _token = deviceToken.Description.Trim('<', '>');
            }
            var message = new RegisterDeviceMessage
            {
                Token = _token,
            };
            WeakReferenceMessenger.Default.Send(message);
        }
    }
}
