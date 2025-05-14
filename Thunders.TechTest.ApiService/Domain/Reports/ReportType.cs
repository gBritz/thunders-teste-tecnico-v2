namespace Thunders.TechTest.ApiService.Domain.Reports;

/// <summary>
/// Defines a type of report.
/// </summary>
public enum ReportType
{
    /// <summary>
    /// Valor total por hora por cidade.
    /// </summary>
    TotalPerHourByCity = 1,

    /// <summary>
    /// As praças que mais faturaram por mês (a quantidade a ser processada deve ser configurável).
    /// </summary>
    TopEarningTollPlazasPerMonth = 2,

    /// <summary>
    /// Quantos tipos de veículos passaram em uma determinada praça.
    /// </summary>
    VehicleClassificationByTollPlaza = 3,
}