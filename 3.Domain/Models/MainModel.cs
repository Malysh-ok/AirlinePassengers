using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.Enums;
using Infrastructure.BaseComponents.Components;

namespace Domain.Models;

/// <summary>
/// Главная модель приложения (бизнес-логика).
/// </summary>
public class MainModel
{
    /// <summary>
    /// Стратегия выбора поставщика репозитория.
    /// </summary>
    private FileRepoStrategy FileRepoStrategy { get; }
    
    /// <summary>
    /// Тип файла репозитория.
    /// </summary>
    public string FileType { get; }

    /// <summary>
    /// Расширение файла репозитория.
    /// </summary>
    public string FileExt { get; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public MainModel()
    {
        FileRepoStrategy = new FileRepoStrategy(FileRepoProviderEnm.Json);
        FileType = FileRepoStrategy.FilesRepo.FileType;
        FileExt = FileRepoStrategy.FilesRepo.FileExt;
    }

    /// <summary>
    /// Получение списка вылетающих пассажиров.
    /// </summary>
    public async Task<Result<IEnumerable<DepartingPassenger>>> GetPassengerListAsync(string fileName)
    {
        // Бизнес-логика элементарна - просто получить данные из репозитория    

        FileRepoStrategy.FilesRepo.FileName = fileName;

        return await FileRepoStrategy.FilesRepo.GetAllAsync<DepartingPassenger>();
    }
    
    /// <summary>
    /// Сохранение списка вылетающих пассажиров.
    /// </summary>
    public async Task<Result<bool>> SetPassengerListAsync(IEnumerable<DepartingPassenger> passengers, string fileName)
    {
        // Бизнес-логика элементарна - просто получить данные из репозитория 
        
        FileRepoStrategy.FilesRepo.FileName = fileName;

        return await FileRepoStrategy.FilesRepo.SetAllAsync(passengers);
    }
}