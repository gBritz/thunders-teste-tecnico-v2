using Thunders.TechTest.ApiService.Domain.Reports;

namespace Thunders.TechTest.Tests.Domain.Reports;

public class ReportTest
{
    [Fact]
    public void GivenReportWithParamters_WhenGetParameterByName_ResultShouldNotBeNull()
    {
        var report = new Report
        {
            FileName = "report.csv",
            FormatType = ReportFormatType.Csv,
            Status = ReportStatusType.New,
            Type = ReportType.TotalPerHourByCity,
            Parameters =
            {
                new()
                {
                    Name = "Parameter_1",
                    Value = "1",
                }
            }
        };

        var parameter = report.GetParameterByName("Parameter_1");

        Assert.NotNull(parameter);
    }

    [Fact]
    public void GivenReportWithoutParamters_WhenGetParameterByName_ResultShouldBeNull()
    {
        var report = new Report
        {
            FileName = "report.csv",
            FormatType = ReportFormatType.Csv,
            Status = ReportStatusType.New,
            Type = ReportType.TotalPerHourByCity,
        };

        var parameter = report.GetParameterByName("Parameter_1");

        Assert.Null(parameter);
    }
}