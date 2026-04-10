using Heroes.Core.Interfaces;


namespace Heroes.Core.Heroes;


/// <summary>
/// Представляет героя-мага, использующего магические атаки.
/// </summary>
public class Mage : HeroBase, IAttacker
{
    
    /// <summary>
    /// Наносит магический урон цели, если маг жив и цель существует.
    /// </summary>
    /// <param name="target">Цель атаки.</param>
    public override void Attack(IDamageable target)
    {
        if (IsAlive && target is not null && target.IsAlive)
        {
            target.TakeDamage(TotalDamage);
        }
    }
}