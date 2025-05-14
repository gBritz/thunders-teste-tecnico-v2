using Thunders.TechTest.ApiService.Domain.Tolls;

namespace Thunders.TechTest.Tests.Domain.Tolls;

public class TollPlazaTest
{
    [Fact]
    public void Test()
    {
        var id = Guid.NewGuid();
        var plaza = TollPlaza.New(id);

        Assert.NotNull(plaza);
        Assert.Equal(id, plaza.Id);
        Assert.Null(plaza.City);
        Assert.Null(plaza.Concessionaire);
        Assert.Null(plaza.Highway);
        Assert.Null(plaza.State);
    }
}