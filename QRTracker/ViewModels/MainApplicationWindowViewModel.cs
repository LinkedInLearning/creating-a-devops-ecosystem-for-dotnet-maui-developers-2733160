using QRTracker.Constants;
using QRTracker.Shared.Interfaces;

namespace QRTracker.ViewModels;
public class MainApplicationWindowViewModel : BaseViewModel
{
    private string _SearchString = string.Empty;
    private bool _AllowSearch = false;
    private IQRService _QRService;

    public MainApplicationWindowViewModel(IQRService qrService)
    {
        _QRService = qrService;
    }

    public string SearchString
    {
        get => _SearchString;
        set
        {
            if (_SearchString != value)
            {
                _SearchString = value;
                OnPropertyChanged();
            }
        }
    }

    public bool AllowSearch
    {
        get
        {
            return _AllowSearch;
        }
        internal set
        {
            if (_AllowSearch != value)
            {
                _AllowSearch = value;
                OnPropertyChanged();
            }
        }
    }

    private Command? _SearchCommand;
    public Command SearchCommand => _SearchCommand ??= new Command(async () => await PerformSearch());

    private async Task PerformSearch()
    {
        if (SearchString != string.Empty)
        {
            var qrCodes = await _QRService.GetQRCodeListAsync();
            var qrCode = qrCodes.FirstOrDefault(qr => qr.Description.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            if (qrCode != null)
            {
                var pageParams = new ShellNavigationQueryParameters();
                pageParams.Add("QRCodeItem", qrCode);
                SearchString = "";
                await Shell.Current.GoToAsync(Routes.QRItemDetailRoute, true, pageParams);
            }
        }
    }

    private Command? _SecondaryCommand;
    public Command SecondaryCommand => _SecondaryCommand ??= new Command(() => ShowSecondaryWindow());

    private Guid? _SecondaryWindowGuid;
    private void ShowSecondaryWindow()
    {
        if (_SecondaryWindowGuid.HasValue && Application.Current!.Windows.Any(w => w.Id == _SecondaryWindowGuid))
        {
            var window = Application.Current!.Windows.First(w => w.Id == _SecondaryWindowGuid);
            Application.Current!.ActivateWindow(window);
        }
        else
        {
            var secondaryWindow = new OtherWindows.SecondaryWindow();
            _SecondaryWindowGuid = secondaryWindow.Id;
            Application.Current!.OpenWindow(secondaryWindow);
        }
    }
}