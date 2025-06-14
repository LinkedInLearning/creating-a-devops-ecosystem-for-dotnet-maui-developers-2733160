using CommunityToolkit.Mvvm.Messaging;
using QRTracker.Constants;
using QRTracker.Helpers;
using QRTracker.Interfaces;
using QRTracker.ViewModels;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace QRTracker;

[RequiresUnreferencedCode("Calls SetInvokeJavaScriptTarget.")]
public partial class App : Application
{
    private MainApplicationWindowViewModel _MainApplicationViewModel;

#if ANDROID || IOS
    private readonly INotificationRegistrationService _notificationRegistrationService;
    public App(MainApplicationWindowViewModel mainWindowViewModel,
        INotificationRegistrationService notificationRegistrationService)
    {
        _notificationRegistrationService = notificationRegistrationService;
#else
    public App(MainApplicationWindowViewModel mainWindowViewModel)
    {
#endif
        InitializeComponent();

        _MainApplicationViewModel = mainWindowViewModel;    
        RegisterRoutes();
        RegisterMessageListeners();
    }

    private void RegisterMessageListeners()
    {
#if ANDROID || IOS
        WeakReferenceMessenger.Default.Register<RegisterDeviceMessage>(this, async (r, m) =>
        {
            if (!await _notificationRegistrationService.RegisterDeviceWithNotificationHub(m))
            {
                Console.WriteLine("Failed to register device with notification hub.");
            }
        });
#endif
    }

    public void RegisterRoutes()
    {
        Routing.RegisterRoute(Routes.QRItemDetailRoute, typeof(QRItemDetailPage));
        Routing.RegisterRoute(Routes.AboutRoute, typeof(AboutPage));
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new MainAppWindow(_MainApplicationViewModel);
    }

    protected override async void OnStart()
    {
        base.OnStart();
#if ANDROID
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.PostNotifications>();
            if (status != PermissionStatus.Granted)
            {
                Console.WriteLine("Notification permission denied.");
            }
        }

        if (status == PermissionStatus.Granted)
        {
            var message = new RegisterDeviceMessage();
            WeakReferenceMessenger.Default.Send(message);
        }
#endif
    }
}