using System;
using System.Collections.Generic;
using Heroes.Core.Heroes;

namespace Heroes.Core.Factory;

/// <summary>
/// Фабрика для создания героев.
/// </summary>
public class HeroFactory
{
    
    /// <summary>
    /// Словарь, хранящий делегаты создания для каждого зарегистрированного типа героя.
    /// </summary>
    private readonly Dictionary<Type, Func<int, int, HeroBase>> _creators = [];

    
    /// <summary>
    /// Регистрирует новый тип героя и способ его создания в фабрике.
    /// </summary>
    /// <typeparam name="T">Тип героя, который должен наследоваться от <see cref="HeroBase"/>.</typeparam>
    /// <param name="creator">Функция, принимающая здоровье и урон, и возвращающая созданный экземпляр героя.</param>
    public void RegisterType<T>(Func<int, int, T> creator) where T : HeroBase
    {
        _creators[typeof(T)] = creator;
    }
    
    /// <summary>
    /// Создает экземпляр героя указанного типа с заданными параметрами.
    /// </summary>
    /// <typeparam name="T">Тип героя, которого нужно создать.</typeparam>
    /// <param name="health">Начальное здоровье героя.</param>
    /// <param name="damage">Базовый урон героя.</param>
    /// <returns>Экземпляр запрошенного героя.</returns>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, если запрошенный тип <typeparamref name="T"/> предварительно не был зарегистрирован с помощью метода <see cref="RegisterType{T}"/>.
    /// </exception>
    public T Create<T>(int health, int damage) where T : HeroBase
    {
        Type requestedType = typeof(T);

        if (_creators.TryGetValue(requestedType, out var creatorFunc))
        {
            return (T)creatorFunc(health, damage);
        }

        throw new InvalidOperationException($"Тип героя {requestedType.Name} не зарегистрирован в фабрике.");
    }
}