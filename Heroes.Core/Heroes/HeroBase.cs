using System.Text.Json.Serialization;
using Heroes.Core.Interfaces;
using Heroes.Core.Weapons;

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
    /// Базовый урон, который герой наносит при атаке без учета оружия.
    /// </summary>
    public int BaseDamage
    {
        get;
        init => field = value < 0 ? 0 : value;
    }

    /// <inheritdoc />
    [JsonIgnore]
    public bool IsAlive => Health > 0;

    /// <summary>
    /// Показывает, мертв ли герой (здоровье равно нулю).
    /// </summary>
    [JsonIgnore]
    public bool IsDead => !IsAlive;

    /// <summary>
    /// Экипированное оружие. Может быть null, если герой безоружен.
    /// </summary>
    [JsonInclude]
    public WeaponBase? EquippedWeapon { get; private set; }

    /// <summary>
    /// Общий урон, включающий базовый урон героя и дополнительный урон от экипированного оружия.
    /// </summary>
    [JsonIgnore] 
    public int TotalDamage => BaseDamage + (EquippedWeapon?.DamageBonus ?? 0);

    /// <summary>
    /// Уменьшает здоровье героя на величину полученного урона.
    /// </summary>
    /// <param name="amount">Количество получаемого урона. Отрицательные значения игнорируются.</param>
    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            Health -= amount;
        }
    }
    
    /// <summary>
    /// Экипирует героя переданным оружием.
    /// </summary>
    /// <param name="weapon">Оружие для экипировки. Можно передать null, чтобы разоружить героя.</param>
    public void EquipWeapon(WeaponBase? weapon)
    {
        EquippedWeapon = weapon;
    }
    
    /// <summary>
    /// Выполняет атаку на указанную цель. Реализация зависит от конкретного класса наследника.
    /// </summary>
    /// <param name="target">Цель для атаки.</param>
    public abstract void Attack(IDamageable target);
}