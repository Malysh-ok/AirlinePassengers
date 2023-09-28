using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using App.ViewModels.Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAccess.Entities;
using Domain.Models;
using Infrastructure.AppComponents.AppExceptions;
using Infrastructure.BaseExtensions;
using Infrastructure.WpfModule.Components.Collections;
using Microsoft.Win32;

namespace App.ViewModels;

/// <summary>
/// Главная модель представления.
/// </summary>
public class MainViewModel : ObservableObject
{
    /// <summary>
    /// Главная модель (бизнес-логика).
    /// </summary>
    private readonly MainModel _mainModel;
    
    /// <summary>
    /// Имя последнего выбранного файла.
    /// </summary>
    private string? _lastOpenFileName;
    
    /// <summary>
    /// Коллекция пассажиров.
    /// </summary>
    public ObservableCollectionEx<DepartingPassenger> Passengers { get; }
    
    /// <summary>
    /// Команда получения списка вылетающих пассажирах.
    /// </summary>
    public ICommand GetPassengerListCommand { get; }
    
    /// <summary>
    /// Команда сохранения списка вылетающих пассажирах.
    /// </summary>
    public ICommand SetPassengerListCommand { get; }
    
    /// <summary>
    /// Команда выхода из приложения.
    /// </summary>
    public ICommand ExitCommand { get; }
    
    private StatusBarData _statusBarData;
    /// <summary>
    /// Объект для привязки данных статус бара.
    /// </summary>
    public StatusBarData StatusBarData
    {
        get => _statusBarData;
        set
        {
            SetProperty(ref _statusBarData, value);
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                SetProperty(ref _statusBarData, new StatusBarData());
            });        
        }
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public MainViewModel()
    {
        // Создаем модель
        _mainModel = new MainModel();
        
        // Пустая коллекция пассажиров
        Passengers = new ObservableCollectionEx<DepartingPassenger>();
        
        // Подключаем к команде получения списка вылетающих пассажирах соответствующий делегат
        GetPassengerListCommand = new RelayCommand(OnGetPassengerList);
        
        // Подключаем к команде сохранения списка вылетающих пассажирах соответствующий делегат
        SetPassengerListCommand = new RelayCommand(OnSetPassengerList);

        // Подключаем к команде выхода из приложения соответствующий делегат
        ExitCommand = new RelayCommand(OnExitApp);

        // Данные для статус-бара 
        _statusBarData = new StatusBarData();
    }

    /// <inheritdoc cref="MainModel.GetPassengerListAsync"/>
    private async void OnGetPassengerList()
    {
        // Диалог открытия файла
        var openFileDialog = new OpenFileDialog()
        {
            FileName = _lastOpenFileName,
            DefaultExt = _mainModel.FileExt,
            Filter = $"{_mainModel.FileType}|*.{_mainModel.FileExt}|Все файлы|*.*",
        };
        if (openFileDialog.ShowDialog() == true)
        {
            // Получаем список вылетающих пассажиров
            var result = await _mainModel.GetPassengerListAsync(openFileDialog.FileName!);

            if (result)
            {
                // Перезаписываем коллекцию Passengers
                Passengers.Clear();
                Passengers.AddRange(result.Value);

                // Пишем в статус-бар
                StatusBarData = new StatusBarData(null, "Получили список авиапассажиров.");
            }
            else
            {
                // Пишем в статус-бар об ошибке
                StatusBarData = new StatusBarData(
                    Brushes.Red,
                    AppException.Create(result.Excptn).Flatten()
                );
            }

            // Сохраняем имя выбранного файла
            _lastOpenFileName = (openFileDialog.FileName);
        }
    }
    
    /// <inheritdoc cref="MainModel.SetPassengerListAsync(System.Collections.Generic.IEnumerable{DataAccess.Entities.DepartingPassenger},string)"/>
    private async void OnSetPassengerList()
    {
        // Диалог сохранения файла
        var saveFileDialog = new SaveFileDialog
        {
            FileName = _lastOpenFileName,
            DefaultExt = _mainModel.FileExt,
            Filter = $"{_mainModel.FileType}|*.{_mainModel.FileExt}|Все файлы|*.*",
            AddExtension = true
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            // Сохраняем список вылетающих пассажиров
            var result = await _mainModel.SetPassengerListAsync(Passengers, saveFileDialog.FileName!);

            if (result)
            {
                // Пишем в статус-бар
                StatusBarData = new StatusBarData(null, "Сохранили список авиапассажиров.");
            }
            else
            {
                // Пишем в статус-бар об ошибке
                StatusBarData = new StatusBarData(
                    Brushes.Red,
                    AppException.Create(result.Excptn).Flatten()
                );
            }

            // Сохраняем имя выбранного файла
             _lastOpenFileName = (saveFileDialog.FileName);
        }
    }

    
    /// <summary>
    /// Выход из приложения.
    /// </summary>
    private void OnExitApp()
    {
        Environment.Exit(0);
    }

}