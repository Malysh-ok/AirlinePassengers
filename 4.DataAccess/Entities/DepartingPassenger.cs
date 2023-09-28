using System.Diagnostics.CodeAnalysis;
using Infrastructure.BaseComponents.Components.Attributes;

namespace DataAccess.Entities;

/// <summary>
/// Данные вылетающего пассажира.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DepartingPassenger
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="departureTimestamp">Время вылета.</param>
    /// <param name="flightNumber">Номер рейса.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <param name="firstName">Имя.</param>
    /// <param name="middleName">Среднее имя (оно же отчество).</param>
    public DepartingPassenger(DateTime departureTimestamp, int flightNumber, 
        string lastName, string firstName, string? middleName = null)
    {
        DepartureTimestamp = departureTimestamp;
        FlightNumber = flightNumber;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    /// <summary>
    /// Время вылета.
    /// </summary>
    [ColumnName("Время вылета")]
    public DateTime DepartureTimestamp { get; protected set; }
    
    /// <summary>
    /// Номер рейса.
    /// </summary>
    [ColumnName("Номер рейса")]
    public int FlightNumber { get; protected set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    [ColumnName("Фамилия")]
    public string LastName { get; set; } = null!;
    
    /// <summary>
    /// Имя.
    /// </summary>
    [ColumnName("Имя")]
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Среднее имя (оно же отчество).
    /// </summary>
    [ColumnName("Отчество")]
    public string? MiddleName { get; set; }
}