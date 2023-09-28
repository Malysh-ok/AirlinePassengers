using DataAccess.Entities;
using Domain.Models;
using Infrastructure.AppComponents.AppExceptions;
using Infrastructure.BaseExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests;

[TestClass]
public class MainTests
{
    [TestMethod]
    public async Task FillPassengersTest()
    {
        var mainModel = new MainModel();

        var passengers = new List<DepartingPassenger>
        {
            new (DateTime.Now, 100, "Иванов", "Иван"),
            new (DateTime.Now, 100, "Петров", "Петр"),
            new (DateTime.Now, 101, "Сидоров", "Сидор"),
            new (DateTime.Now, 101, "Павлова", "Ольга"),
            new (DateTime.Now, 102, "Алексеева", "Ирина"),
        };

        var fileName = @"D:\Passengers.json";

        // Сохраняем список вылетающих пассажиров
        var result = await mainModel.SetPassengerListAsync(passengers, fileName);

        if (result)
        {
            // Все ок
            Console.WriteLine(@"Сохранили список авиапассажиров.");
        }
        else
        {
            // Ошибка
            Console.WriteLine(AppException.Create(result.Excptn).Flatten());
        }
    }
}