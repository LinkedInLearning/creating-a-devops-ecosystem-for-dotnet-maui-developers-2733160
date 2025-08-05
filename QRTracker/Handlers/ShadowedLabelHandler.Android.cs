
using Android.Views;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Handlers;

namespace QRTracker.Handlers;

public partial class ShadowedLabelHandler : LabelHandler
{
    protected override AppCompatTextView CreatePlatformView()
    {
        var view = base.CreatePlatformView();

        view.SetLayerType(LayerType.Software, null);
        view.SetShadowLayer(50, 2, 2, Android.Graphics.Color.DarkGray);
        return view;
    }

    protected override void DisconnectHandler(AppCompatTextView platformView)
    {
        base.DisconnectHandler(platformView);
    }

}
