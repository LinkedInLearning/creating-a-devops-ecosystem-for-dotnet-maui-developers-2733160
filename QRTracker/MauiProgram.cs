using QRTracker.DataAccess.Mock.Services;
using QRTracker.Shared.Interfaces;
using QRTracker.ViewModels;
using Microsoft.Extensions.Logging;
using QRTracker.Handlers;
using System.Diagnostics.CodeAnalysis;

namespace QRTracker;

[RequiresUnreferencedCode("Calls SetInvokeJavaScriptTarget.")]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMauiHandlers((handlers) => {
                handlers.AddHandler(typeof(ShadowedLabel), typeof(ShadowedLabelHandler));
         });
        AddDependancyInjection(builder);

#if DEBUG
        builder.Logging.AddDebug();
#endif

#if IOS || MACCATALYST
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler<Microsoft.Maui.Controls.CollectionView, Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2>();
            handlers.AddHandler<Microsoft.Maui.Controls.CarouselView, Microsoft.Maui.Controls.Handlers.Items2.CarouselViewHandler2>();
        });
#endif

        return builder.Build();
    }

    private static void AddDependancyInjection(MauiAppBuilder builder)
    {
        builder.Services.AddScoped<IQRService, QRService>();

        builder.Services.AddScoped<MainViewModel>();
        builder.Services.AddScoped<QRItemDetailViewModel>();
        builder.Services.AddScoped<MainApplicationWindowViewModel>();
    }
}
