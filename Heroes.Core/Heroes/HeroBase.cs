using Heroes.Core.Interfaces;

namespace Heroes.Core.Heroes;

public abstract class HeroBase : IDamageable, IAttacker
{
    public int Health
    {
        get;
        protected set => field = value < 0 ? 0 : value;
    }

    public int Damage
    {
        get;
        init => field = value < 0 ? 0 : value;
    }

    public bool IsAlive => Health > 0;
    public bool IsDead => !IsAlive;

    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            Health -= amount;
        }
    }

    public abstract void Attack(IDamageable target);
}