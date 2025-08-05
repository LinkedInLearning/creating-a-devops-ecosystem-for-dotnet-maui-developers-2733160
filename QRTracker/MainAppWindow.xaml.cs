using QRTracker.ViewModels;

namespace QRTracker;

public partial class MainAppWindow : Window, IDisposable
{
	public MainAppWindow(MainApplicationWindowViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
    }

    protected override void OnParentSet()
    {
#if WINDOWS
        Shell.Current.Navigated -= Current_Navigated;
        Shell.Current.Navigated += Current_Navigated;
#endif
        base.OnParentSet();
    }

    private void Current_Navigated(object? sender, ShellNavigatedEventArgs e)
    {
        var viewModel = (MainApplicationWindowViewModel)this.BindingContext;

        var allowSearch = Shell.Current == null ? false : Shell.Current.CurrentPage.GetType() == typeof(MainPage);
        viewModel.AllowSearch = allowSearch;
    }

    public void Dispose()
    {
        Shell.Current.Navigated -= Current_Navigated;
    }
}