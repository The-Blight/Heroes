namespace Heroes.Core.Interfaces;

/// <summary>
/// Определяет контракт для сущности, способной наносить урон другим объектам.
/// </summary>
public interface IAttacker
{
    
    /// <summary>
    /// Выполняет атаку на указанную цель.
    /// </summary>
    /// <param name="target">Цель атаки, реализующая интерфейс <see cref="IDamageable"/>.</param>
    void Attack(IDamageable target); 

}