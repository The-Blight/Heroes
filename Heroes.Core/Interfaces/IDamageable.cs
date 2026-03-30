namespace Heroes.Core.Interfaces;

public interface IDamageable
{
    int Health { get; }
    bool IsAlive { get; }
    void TakeDamage(int amount);
}