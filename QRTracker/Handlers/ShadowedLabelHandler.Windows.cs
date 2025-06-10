using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using System.Data;
using System.Numerics;
using Windows.Security.Cryptography.Certificates;

namespace QRTracker.Handlers;

public partial class ShadowedLabelHandler : LabelHandler
{
    TextBlock? winView;
    protected override TextBlock CreatePlatformView()
    {
        winView = base.CreatePlatformView();


        var compositor = ElementCompositionPreview.GetElementVisual(winView).Compositor;

        var dropShadow = compositor.CreateDropShadow();
        var shadowVisual = compositor.CreateSpriteVisual();
        shadowVisual.Shadow = dropShadow;

        Vector2 newSize = new Vector2(0, 0);
        if (winView is FrameworkElement contentFE)
        {
            newSize = new Vector2((float)winView.ActualWidth, (float)winView.ActualHeight);
        }
        shadowVisual.Size = newSize;

        dropShadow.Color = Windows.UI.Color.FromArgb(75, 128, 128, 128);
        dropShadow.BlurRadius = 8;
        dropShadow.Opacity = 20f;
        dropShadow.Offset = new System.Numerics.Vector3(2, 2, 0);

        ElementCompositionPreview.SetElementChildVisual(winView, shadowVisual);

        return winView;
    }

    public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
    {
        SpriteVisual shadowVisual = (SpriteVisual)ElementCompositionPreview.GetElementChildVisual(winView);
        Vector2 newSize = new Vector2(0, 0);
        if (winView is FrameworkElement contentFE)
        {
            newSize = new Vector2((float)winView.ActualWidth, (float)winView.ActualHeight);
        }
        shadowVisual.Size = newSize;
        return base.GetDesiredSize(widthConstraint, heightConstraint);
    }


    protected override void DisconnectHandler(TextBlock platformView)
    {
        base.DisconnectHandler(platformView);
    }
}
