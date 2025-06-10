using Android.App;
using Android.Runtime;
using System.Diagnostics.CodeAnalysis;

namespace QRTracker;

[Application]
[RequiresUnreferencedCode("Calls SetInvokeJavaScriptTarget.")]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
