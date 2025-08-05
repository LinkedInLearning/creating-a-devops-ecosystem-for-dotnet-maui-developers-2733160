
using QRTracker.Shared.Models;

namespace QRTracker.Shared.Interfaces;
public interface IQRService
{
    public Task<QRCodeList> GetQRCodeListAsync();
}
