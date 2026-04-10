using System.Text.Json.Serialization;

namespace Heroes.Core.Weapons;

/// <summary>Базовый класс для всех оружий</summary>
[JsonDerivedType(typeof(Sword), typeDiscriminator: "Sword")]
[JsonDerivedType(typeof(Staff), typeDiscriminator: "Staff")]
public abstract class WeaponBase
{
    /// <summary>Название оружия.</summary>
    public abstract string Name { get; }
    
    /// <summary>Дополнительный урон от оружия. </summary>
    public abstract int DamageBonus { get; }
}