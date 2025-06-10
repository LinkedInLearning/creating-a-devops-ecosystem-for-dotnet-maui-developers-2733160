
using Microsoft.Maui.Controls;
using QRTracker.Constants;
using QRTracker.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace QRTracker;

public partial class AboutPage : ContentPage
{
    Color _SelectedDarkColor = App.Current?.Resources.GetResource<Color>("SecondaryDarkText") ?? Colors.LightGray;

    [RequiresUnreferencedCode("Calls SetInvokeJavaScriptTarget.")]
    public AboutPage()
    {
        InitializeComponent();
        hwvAbout.SetInvokeJavaScriptTarget(this);

        btnAbout.SetAppThemeColor(Button.TextColorProperty, Colors.Black, _SelectedDarkColor);
        btnEULA.SetAppThemeColor(Button.TextColorProperty, Colors.Gray, Colors.LightGray);
    }

    private async void btnAbout_Clicked(object sender, EventArgs e)
    {
        btnAbout.SetAppThemeColor(Button.TextColorProperty, Colors.Black, _SelectedDarkColor);
        btnEULA.SetAppThemeColor(Button.TextColorProperty, Colors.Gray, Colors.LightGray);

        await hwvAbout.InvokeJavaScriptAsync<string>("gotoAbout", HybridJsContext.Default.String);
    }

    private async void btnEULA_Clicked(object sender, EventArgs e)
    {
        btnAbout.SetAppThemeColor(Button.TextColorProperty, Colors.Gray, Colors.LightGray);
        btnEULA.SetAppThemeColor(Button.TextColorProperty, Colors.Black, _SelectedDarkColor);

        var eulaAccepted = await GetEulaAccepted();

        await hwvAbout.InvokeJavaScriptAsync<string>("gotoEULA", HybridJsContext.Default.String, [eulaAccepted], [HybridJsContext.Default.Boolean]);
    }

    private async Task<bool> GetEulaAccepted()
    {
        bool returnValue = false;
        try
        {
            string? acceptedEula = await SecureStorage.Default.GetAsync(Storage.ACCEPTED_EULA);
            if (!string.IsNullOrEmpty(acceptedEula))
            {
                returnValue = Convert.ToBoolean(acceptedEula);
            }
        }
        catch
        {
            // add appropriate error handling
        }
        return returnValue;
    }

    public async Task SetEulaAccepted(bool accepted)
    {
        try
        {
            await SecureStorage.Default.SetAsync(Storage.ACCEPTED_EULA, accepted.ToString());
        }
        catch
        {
            // add appropriate error handling
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(bool))]
    internal partial class HybridJsContext : JsonSerializerContext { }

}