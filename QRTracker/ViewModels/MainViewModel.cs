
using QRTracker.Shared.Interfaces;
using QRTracker.Shared.Models;

namespace QRTracker.ViewModels;
public class MainViewModel : BaseViewModel
{
    private IQRService _QRService;

    private QRCodeList? _QRCodeList = null;

    public MainViewModel(IQRService qrService)
    {
        _QRService = qrService;
    }

    public async Task LoadDataAsync()
    {
        QRCodeList = await _QRService.GetQRCodeListAsync();
    }

    public QRCodeList? QRCodeList
    {
        get => _QRCodeList;
        set
        {
            if (_QRCodeList != value)
            {
                _QRCodeList = value;
                OnPropertyChanged();
            }
        }
    }
}
