using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.Tests.Domain.Reports;

public class ReportParameterTest
{
    [Fact]
    public void GivenIntValueAsString_WhenCallValueAsInt_ResultShouldBeInt32()
    {
        var parameter = new ReportParameter
        {
            Name = "Test",
            Value = "1",
        };

        Assert.Equal(1, parameter.ValueAsInt());
    }

    [Fact]
    public void GivenLongValueAsString_WhenCallValueAsLong_ResultShouldBeInt64()
    {
        var parameter = new ReportParameter
        {
            Name = "Test",
            Value = long.MaxValue.ToString(),
        };

        Assert.Equal(long.MaxValue, parameter.ValueAsLong());
    }

    [Theory]
    [MemberData(nameof(ListOfGuids))]
    public void GivenArrayOfGuidAsString_WhenCallValueAsLong_ResultShouldBeInt64(
        Guid[] values)
    {
        var parameter = new ReportParameter
        {
            Name = "Test",
            Value = string.Join(',', values.Select(v => v.ToString())),
        };

        Assert.Equal(values, parameter.ValueAsArrayOfGuid());
    }

    public static IEnumerable<object[]> ListOfGuids()
    {
        return new List<object[]>
        {
            new object[] { new object[0] },
            new object[] { new object[] { Guid.NewGuid() } },
            new object[] { new object[] { Guid.NewGuid(), Guid.NewGuid() } },
        };
    }
}