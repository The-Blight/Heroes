using Heroes.Core.Interfaces;


namespace Heroes.Core.Heroes;

public class Warrior : HeroBase
{
    public override void Attack(IDamageable target)
    {
        if (IsAlive && target is not null && target.IsAlive)
        {
            target.TakeDamage(Damage);
        }
    }
}

