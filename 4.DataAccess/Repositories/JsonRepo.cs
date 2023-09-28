using System.Net;
using DataAccess.Repositories.Interfaces;
using Infrastructure.BaseComponents.Components;
using Newtonsoft.Json;

namespace DataAccess.Repositories;

/// <summary>
/// JSON-репозиторий.
/// </summary>
public class JsonRepo : IFilesRepo
{
    /// <inheritdoc />
    public string FileType => "Документ JSON";

    /// <inheritdoc />
    public string FileExt => "json";

    /// <inheritdoc />
    public string FileName { get; set; }

    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private JsonRepo()
    {
        FileName = Path.Combine(Path.GetTempPath(), $"tmp{DateTime.Now.Ticks}.json");;
    }

    /// <inheritdoc />
    public static IFilesRepo Create(string? fileName = null)
    {
        var jsonRepo = new JsonRepo();
        if (fileName is not null)
            jsonRepo.FileName = fileName;

        return jsonRepo;
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync<TEntity>()
        where TEntity : class
    {
        try
        {
            using var sr = new StreamReader(FileName, System.Text.Encoding.Default);
            var entitiesStr = await sr.ReadToEndAsync();
            sr.Close();

            var entities = JsonConvert.DeserializeObject<List<TEntity>>(entitiesStr)
                           ?? new List<TEntity>();

            return await Task.FromResult(Result<IEnumerable<TEntity>>.Done(entities));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Result<IEnumerable<TEntity>>.Fail(ex));
        }
    }

    /// <inheritdoc />
    public async Task<Result<bool>> SetAllAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        try
        {
            var jss = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,

            };
            var entitiesStr = JsonConvert.SerializeObject(entities, jss);

            await using var sw = new StreamWriter(FileName, false, System.Text.Encoding.Default);
            await sw.WriteAsync(entitiesStr);
            sw.Close();
            
            return await Task.FromResult(Result<bool>.Done(true));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Result<bool>.Fail(ex));
        }
    }
}