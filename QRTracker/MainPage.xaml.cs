using QRTracker.Constants;
using QRTracker.Shared.Models;
using QRTracker.ViewModels;

namespace QRTracker;

public partial class MainPage : ContentPage, IQueryAttributable
{
    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();

        base.BindingContext = mainViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext != null && ((MainViewModel)BindingContext).QRCodeList == null)
        {
            var bindingContext = (MainViewModel)BindingContext;
            await bindingContext.LoadDataAsync();
        }
    }

    public async void QRItemSelected(object sender, TappedEventArgs args)
    {
        if (args.Parameter != null)
        {
            var pageParams = new ShellNavigationQueryParameters();
            QRCodeItem model = (QRCodeItem)args.Parameter;
            pageParams.Add("QRCodeItem", model.Clone());
            await Shell.Current.GoToAsync(Routes.QRItemDetailRoute, true, pageParams);
        }
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.AboutRoute, true);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query != null && query.ContainsKey("UpdatedQRCode"))
        {
            UpdatedQRCodeItem = query["UpdatedQRCode"] as QRCodeItem;
        }
    }

    public QRCodeItem? UpdatedQRCodeItem
    {
        set
        {
            if (value != null)
            {
                var viewModel = ((MainViewModel)this.BindingContext);

                if (viewModel.QRCodeList == null)
                {
                    return;
                }
                var oldItem = viewModel.QRCodeList.SingleOrDefault(x => x.Id == value.Id);
                if (oldItem != null)
                {
                    var index = viewModel.QRCodeList.IndexOf(oldItem);
                    viewModel.QRCodeList.Remove(oldItem);
                    viewModel.QRCodeList.Insert(index, value);
                    this.BindingContext = null;
                    this.BindingContext = viewModel;
                }
            }
        }
    }
}
