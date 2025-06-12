
using Dynatrace.MAUI;
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
#if ANDROID || IOS
        IRootAction myAction = Dynatrace.MAUI.Agent.Instance.EnterAction("QR Item Updated");
        try
        {
#endif
            var pageParams = new ShellNavigationQueryParameters();
            if (QRCodeItem != null)
            {
                pageParams.Add("UpdatedQRCode", QRCodeItem);
#if ANDROID || IOS
                myAction.ReportValue("Item Id", QRCodeItem.Id);
#endif
            }

            await Shell.Current.GoToAsync("..", true, pageParams);
#if ANDROID || IOS
        }
        catch (Exception ex)
        {
            myAction.ReportError(ex.Message, ex.HResult);
            throw;
        }
        finally
        {
            myAction.LeaveAction();
        }
#endif
    }

    private Command? _CancelCommand;

    public Command CancelCommand => _CancelCommand ??= new Command(async () => await OnCancel());

    private async Task OnCancel()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}
