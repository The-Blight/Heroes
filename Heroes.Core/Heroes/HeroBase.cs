namespace Heroes.Core.Heroes;

public abstract class HeroBase
{
    public int Health
    {
        get;
        set => field = value < 0 ? 0 : value; //TODO: вынести в метод валидации
    }

    public int Damage
    {
        get;
        init => field = value < 0 ? 0 : value; //TODO: вынести в метод валидации
    }
    
    public bool IsAlive => this.Health > 0 ;
    public bool IsDead => !this.IsAlive;

    public abstract void Attack(HeroBase enemy);
}