using QRTracker.Shared.Models;
using QRTracker.ViewModels;

namespace QRTracker;

public partial class QRItemDetailPage : ContentPage, IQueryAttributable
{
	public QRItemDetailPage(QRItemDetailViewModel qrItemViewModel)
	{
		InitializeComponent();
		this.BindingContext = qrItemViewModel;
    }

    public QRCodeItem? QRCodeItem 
	{
		set
		{
			var viewModel = ((QRItemDetailViewModel)this.BindingContext);

            if (value != viewModel.QRCodeItem)
			{
                viewModel.QRCodeItem = value;
            }
		}
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query != null && query.ContainsKey("QRCodeItem"))
        {
            QRCodeItem = query["QRCodeItem"] as QRCodeItem;
        }
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        urlLabel.Handler?.DisconnectHandler();
        //urlDescription.Handler?.DisconnectHandler();
    }
}