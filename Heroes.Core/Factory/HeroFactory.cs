using System;
using System.Collections.Generic;
using Heroes.Core.Heroes;

namespace Heroes.Core.Factory;

public class HeroFactory
{
    private readonly Dictionary<Type, Func<int, int, HeroBase>> _creators = [];

    public void RegisterType<T>(Func<int, int, T> creator) where T : HeroBase
    {
        _creators[typeof(T)] = creator;
    }

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