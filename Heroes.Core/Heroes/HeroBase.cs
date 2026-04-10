using System.Text.Json.Serialization;
using Heroes.Core.Interfaces;

namespace Heroes.Core.Heroes;

/// <summary>
/// Базовый класс для всех героев. Управляет общим состоянием (здоровье, урон) 
/// и базовой логикой получения урона.
/// </summary>



[JsonDerivedType(typeof(Mage), typeDiscriminator: "Mage")]
[JsonDerivedType(typeof(Warrior), typeDiscriminator: "Warrior")]
public abstract class HeroBase : IDamageable, IAttacker
{
    
    /// <summary>
    /// Текущее здоровье героя. Не может быть меньше нуля.
    /// Изменяется извне только через метод <see cref="TakeDamage"/>.
    /// </summary>
    public int Health
    {
        get;
         set => field = value < 0 ? 0 : value;
    }

    /// <summary>
    /// Базовый урон, который герой наносит при атаке.
    /// </summary>
    public int Damage
    {
        get;
        init => field = value < 0 ? 0 : value;
    }

    /// <inheritdoc/>>
    [JsonIgnore]
    public bool IsAlive => Health > 0;

    /// <summary>Показывает, мертв ли герой.</summary>
    [JsonIgnore]
    public bool IsDead => !IsAlive;

    
    /// <summary>
    /// Уменьшает здоровье героя на величину полученного урона.
    /// </summary>
    /// <param name="amount">Количество урона. Отрицательные значения игнорируются.</param>
    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            Health -= amount;
        }
    }

    /// <summary>
    /// Выполняет атаку на цель. Реализация зависит от конкретного класса наследника.
    /// </summary>
    /// <param name="target">Цель для атаки.</param>
    public abstract void Attack(IDamageable target);
}