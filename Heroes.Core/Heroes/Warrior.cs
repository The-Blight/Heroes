using Heroes.Core.Interfaces;


namespace Heroes.Core.Heroes;


/// <summary>
/// Представляет героя-воина, использующего физические атаки в ближнем бою.
/// </summary>
public class Warrior : HeroBase
{
    
    /// <summary>
    /// Наносит физический урон цели мечом, если воин жив и цель существует.
    /// </summary>
    /// <param name="target">Цель атаки.</param>
    public override void Attack(IDamageable target)
    {
        if (IsAlive && target is not null && target.IsAlive)
        {
            target.TakeDamage(Damage);
        }
    }
}

