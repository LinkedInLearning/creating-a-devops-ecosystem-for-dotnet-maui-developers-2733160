
using QRTracker.Constants;
using QRTracker.Shared.Models;

namespace QRTracker.ViewModels;
public class QRItemDetailViewModel : BaseViewModel
{
    private QRCodeItem? _QRCodeItem = null;
    public QRCodeItem? QRCodeItem
    {
        get => _QRCodeItem;
        set
        {
            if (_QRCodeItem != value)
            {
                _QRCodeItem = value;
                OnPropertyChanged();
            }
        }
    }

    private Command? _SaveCommand;

    public Command SaveCommand => _SaveCommand ??= new Command(async () => await OnSave());

    private async Task OnSave()
    {
        var pageParams = new ShellNavigationQueryParameters();
        if (QRCodeItem != null)
        {
            pageParams.Add("UpdatedQRCode", QRCodeItem);
        }

        await Shell.Current.GoToAsync("..", true, pageParams);
    }

    private Command? _CancelCommand;

    public Command CancelCommand => _CancelCommand ??= new Command(async () => await OnCancel());

    private async Task OnCancel()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}
