using System;
using Heroes.Core.Heroes;

namespace Heroes.Core;

public enum HeroType
{
    Warrior, Mage
    }

public static class HeroFactory
{
    public static HeroBase CreateHero(HeroType type, int health, int damage)
    {
        return type switch
        {
           HeroType.Mage => new Mage()
           {
               Health = health,
               Damage = damage
           },
           HeroType.Warrior => new Warrior()
           {
               Health = health,
               Damage = damage
           },
           _ => throw new ArgumentOutOfRangeException(nameof(type), type, null) //TODO: Прудумать свой тип исключения
        };
    }
}