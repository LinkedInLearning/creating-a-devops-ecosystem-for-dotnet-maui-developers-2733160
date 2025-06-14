using CommunityToolkit.Mvvm.Messaging;
using QRTracker.Constants;
using QRTracker.Helpers;
using QRTracker.Interfaces;
using QRTracker.ViewModels;
using System.Diagnostics.CodeAnalysis;

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
}