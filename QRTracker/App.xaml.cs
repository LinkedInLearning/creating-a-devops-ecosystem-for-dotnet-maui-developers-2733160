using NewRelic.MAUI.Plugin;
using QRTracker.Constants;
using QRTracker.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace QRTracker;

[RequiresUnreferencedCode("Calls SetInvokeJavaScriptTarget.")]
public partial class App : Application
{
    private MainApplicationWindowViewModel _MainApplicationViewModel;
    public App(MainApplicationWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();

        _MainApplicationViewModel = mainWindowViewModel;    
        RegisterRoutes();
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

    protected override void OnStart()
    {
        base.OnStart();

#if ANDROID || IOS
        CrossNewRelic.Current.HandleUncaughtException();
        CrossNewRelic.Current.TrackShellNavigatedEvents();

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            CrossNewRelic.Current.Start("AAc8d70a7b0569978cde1d413573e926dda7f6bd73-NRMA");
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            CrossNewRelic.Current.Start("AAbb6d824f67f3581be18e666ef4cb731270042057-NRMA");
        }
#endif
    }
}