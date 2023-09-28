using System;
using System.Diagnostics.CodeAnalysis;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions;

/// <summary>
/// Базовый класс исключений приложения.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class AppException : Exception
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private AppException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static AppException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<AppException>($"Unexpected error.",
            innerException, "ru", $"Непредвиденная ошибка.");
    }
}