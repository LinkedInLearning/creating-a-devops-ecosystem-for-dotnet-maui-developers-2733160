using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace QRTracker.Handlers;

public partial class ShadowedLabelHandler : LabelHandler
{

    protected override MauiLabel CreatePlatformView()
    {
        var view = base.CreatePlatformView();

        view.ShadowColor = UIColor.Gray;
        view.ShadowOffset = new Size(2, 2);

        return view;
    }

    protected override void DisconnectHandler(MauiLabel platformView)
    {
        base.DisconnectHandler(platformView);
    }
}
