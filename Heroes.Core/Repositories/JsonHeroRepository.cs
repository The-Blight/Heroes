using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Heroes.Core.Heroes;
using Heroes.Core.Repositories.Interfaces;

namespace Heroes.Core.Repositories;

/// <summary>
/// Репозиторий для сохранения и загрузки состояния героев в формате JSON.
/// </summary>
public class JsonHeroRepository : IHeroRepository
{
    private readonly string _saveDirectory;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="JsonHeroRepository"/>.
    /// </summary>
    /// <param name="saveDirectory">Путь к директории, где будут храниться файлы сохранений. По умолчанию - папка "Saves".</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="saveDirectory"/> равен null.</exception>
    public JsonHeroRepository(string saveDirectory = "Saves")
    {
        _saveDirectory = saveDirectory ?? throw new ArgumentNullException(nameof(saveDirectory));
        InitializeStorage(_saveDirectory);
    }

    /// <inheritdoc />
    public void Save(IEnumerable<HeroBase> heroes, string saveName)
    {
        if (heroes is null)
            throw new ArgumentNullException(nameof(heroes));

        var filePath = GetFilePath(CheckNullOrWhiteSpace(saveName));
        string json = JsonSerializer.Serialize(heroes, _jsonOptions);

        File.WriteAllText(filePath, json);
    }

    /// <inheritdoc />
    public IEnumerable<HeroBase> Load(string saveName)
    {
        var filePath = GetFilePath(CheckNullOrWhiteSpace(saveName));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Файл сохранений '{saveName}' не найден по пути: {filePath}");

        string json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<IEnumerable<HeroBase>>(json, _jsonOptions) ?? [];
    }

    /// <summary>
    /// Проверяет наличие директории для сохранений и создает её при необходимости.
    /// </summary>
    /// <param name="saveDirectory">Путь к директории.</param>
    private void InitializeStorage(string saveDirectory)
    {
        if (!Directory.Exists(saveDirectory))
            Directory.CreateDirectory(saveDirectory);
    }


    /// <summary>
    /// Проверяет строку на null, пустоту и наличие только пробельных символов.
    /// </summary>
    /// <param name="value">Значение для проверки.</param>
    /// <returns>Исходную строку, если она валидна.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если значение null, пустое или состоит только из пробелов.</exception>
    private static string CheckNullOrWhiteSpace(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Имя сохранения не может быть пустым.", nameof(value));
        return value;
    }

    /// <summary>
    /// Формирует полный путь к файлу сохранения на основе его имени.
    /// </summary>
    /// <param name="saveName">Имя сохранения (без расширения).</param>
    /// <returns>Полный путь к JSON файлу.</returns>
    private string GetFilePath(string saveName) => Path.Combine(_saveDirectory, $"{saveName}.json");
}