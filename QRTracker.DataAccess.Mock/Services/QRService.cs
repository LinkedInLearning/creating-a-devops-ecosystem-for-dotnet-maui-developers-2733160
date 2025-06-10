using QRTracker.Shared.Interfaces;
using QRTracker.Shared.Models;

namespace QRTracker.DataAccess.Mock.Services;
public class QRService : IQRService
{
    public Task<QRCodeList> GetQRCodeListAsync()
    {
        var tcs = new TaskCompletionSource<QRCodeList>();

        tcs.SetResult(new QRCodeList
        {
            new QRCodeItem { Id=1, URL = "https://dotnet.microsoft.com/en-us/apps/maui", Description="Maui Home" },
            new QRCodeItem { Id=2, URL = "https://Linkedin.com/in/JamesMontemagno", Description="James' LinkedIn" },
            new QRCodeItem { Id=3, URL = "https://Linkedin.com/in/KevinEFord", Description="Kevin's LinkedIn" },
        });
        return tcs.Task;
    }
}
