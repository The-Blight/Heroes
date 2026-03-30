namespace Heroes.Core.Interfaces;


/// <summary>
/// Определяет контракт для любой сущности в игре, которая имеет запас здоровья и может быть атакована.
/// </summary>
public interface IDamageable
{
    /// <summary>Текущее количество очков здоровья.</summary>
    int Health { get; }
    /// <summary>Показывает, жива ли сущность (здоровье больше нуля).</summary>
    bool IsAlive { get; }
    
    /// <summary>
    /// Наносит указанный урон сущности, уменьшая ее здоровье.
    /// </summary>
    /// <param name="amount">Количество получаемого урона. Должно быть положительным числом.</param>
    void TakeDamage(int amount);
}