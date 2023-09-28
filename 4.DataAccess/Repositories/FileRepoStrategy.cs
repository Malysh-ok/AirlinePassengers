using System.Diagnostics;
using DataAccess.Repositories.Enums;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories;

/// <summary>
/// Стратегия выбора поставщика репозитория.
/// </summary>
public class FileRepoStrategy
{
    /// <summary>
    /// Поставщик репозитория.
    /// </summary>
    public FileRepoProviderEnm FileRepoProvider { get; }
    
    /// <summary>
    /// Репозиторий.
    /// </summary>
    public IFilesRepo FilesRepo { get; }
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="fileRepoProvider">Поставщик репозитория.</param>
    public FileRepoStrategy(FileRepoProviderEnm fileRepoProvider)
    {
        FileRepoProvider = fileRepoProvider;
        FilesRepo = fileRepoProvider switch
        {
            FileRepoProviderEnm.Json => JsonRepo.Create(),
            FileRepoProviderEnm.Xml => XmlRepo.Create(),
            _ => throw new ArgumentOutOfRangeException(nameof(fileRepoProvider), fileRepoProvider, null)
        };
    }
}