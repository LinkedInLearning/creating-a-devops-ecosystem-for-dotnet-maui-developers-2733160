using QRTracker.DataAccess.Mock.Services;

namespace QRTracker.Tests;

[TestClass]
public sealed class MockTests
{
    [TestMethod]
    public async Task CheckIfServiceReturnsValue()
    {
        var service = new QRService();

        var result = await service.GetQRCodeListAsync();

        Assert.IsNotNull(result, "Service returned null");
        Assert.IsTrue(result.Count > 0, "Service returned empty list");
    }
}
