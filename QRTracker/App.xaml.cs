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
}