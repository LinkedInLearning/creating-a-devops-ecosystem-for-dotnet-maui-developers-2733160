
using Android.App;
using Android.Views.InputMethods;
using Firebase.Messaging;
using AndroidApp = Android.App;
using AndroidRes = Android.Resource;

namespace QRTracker.Platforms.Android;

[Service(Exported = false)]
[IntentFilter(new[] {"com.google.firebase.MESSAGING_EVENT"})]
public class FirebasePushService : FirebaseMessagingService
{
    int messageId = 0;

    public override void OnMessageReceived(RemoteMessage message)
    {
        base.OnMessageReceived(message);

        var notificaiton = message.GetNotification();

        if (AndroidApp.Application.Context != null &&
            notificaiton != null &&
            OperatingSystem.IsAndroidVersionAtLeast(33))
        {
            var manager = (NotificationManager?)AndroidApp.Application.Context.GetSystemService(NotificationService);
            if (manager != null)
            {
                var channel = new AndroidApp.NotificationChannel(notificaiton.ChannelId ?? "QRTracker", "QRTracker", NotificationImportance.Max);
                manager.CreateNotificationChannel(channel);

                var displayNotification = new AndroidApp.Notification.Builder(AndroidApp.Application.Context, channel.Id)
                    .SetContentTitle(notificaiton.Title)
                    .SetContentText(notificaiton.Body)
                    .SetSmallIcon(AndroidRes.Mipmap.SymDefAppIcon)
                    .Build();
                manager.Notify(messageId++, displayNotification);
            }
        }
    }
}
