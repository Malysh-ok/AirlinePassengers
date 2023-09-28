using System.Windows.Media;

namespace App.ViewModels.Common;

/// <summary>
/// Данные для статус-бара.
/// </summary>
public class StatusBarData
{
    /// <summary>
    /// Кисть по умолчанию.
    /// </summary>
    private readonly Brush _defBrush = Brushes.Blue;
        
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="brush"></param>
    /// <param name="text"></param>
    public StatusBarData(Brush? brush = null, string? text = null)
    {
        Brush = brush ?? _defBrush;
        Text = text ?? string.Empty;
    }

    /// <summary>
    /// Кисть для текста статус-бара.
    /// </summary>
    public Brush? Brush { get; set; }
        
    /// <summary>
    /// Текст статус-бара.
    /// </summary>
    public string? Text { get; set; }
}
