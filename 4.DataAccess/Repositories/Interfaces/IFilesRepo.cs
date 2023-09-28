using Infrastructure.BaseComponents.Components;

namespace DataAccess.Repositories.Interfaces;

/// <summary>
/// Интерфейс файлового репозитория.
/// </summary>
public interface IFilesRepo
{
    /// <summary>
    /// Тип файла репозитория.
    /// </summary>
    public string FileType { get; }

    /// <summary>
    /// Расширение файла репозитория.
    /// </summary>
    public string FileExt { get; }
    
    /// <summary>
    /// Наименование файла репозитория.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Создает новый экземпляр репозитория <see cref="IFilesRepo" /> (фабричный метод).
    /// </summary>
    public static abstract IFilesRepo Create(string? fileName = null);

    /// <summary>
    /// Получает все сущности из репозитория (файла) в виде коллекции.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public Task<Result<IEnumerable<TEntity>>> GetAllAsync<TEntity>()
        where TEntity : class;

    /// <summary>
    /// Сохраняет коллекцию сущностей в репозитории (файле).
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public Task<Result<bool>> SetAllAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class;
}