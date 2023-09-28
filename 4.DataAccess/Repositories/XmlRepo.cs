using DataAccess.Repositories.Interfaces;
using Infrastructure.BaseComponents.Components;

namespace DataAccess.Repositories;

/// <summary>
/// XML-репозиторий.
/// </summary>
public class XmlRepo : IFilesRepo
{
    /// <inheritdoc />
    public string FileType => "Документ XML";

    /// <inheritdoc />
    public string FileExt => "xml";

    /// <inheritdoc />
    public string FileName { get; set; }
    
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private XmlRepo()
    {
        FileName = Path.Combine(Path.GetTempPath(), $"tmp{DateTime.Now.Ticks}.json");;
    }

    /// <inheritdoc />
    public static IFilesRepo Create(string? fileName = null)
    {
        var xmlRepo = new XmlRepo();
        if (fileName is not null)
            xmlRepo.FileName = fileName;

        return xmlRepo;
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync<TEntity>()
        where TEntity : class
    {
        return await Task.FromResult(Result<IEnumerable<TEntity>>.Fail(
            new NotImplementedException()
        ));

    }

    /// <inheritdoc />
    public async Task<Result<bool>> SetAllAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        return await Task.FromResult(Result<bool>.Fail(
            new NotImplementedException()
        ));
    }
}