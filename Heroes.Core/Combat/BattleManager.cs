using System.Collections.Generic;
using System.Linq;
using Heroes.Core.Heroes;
using Heroes.Core.Interfaces;

namespace Heroes.Core.Combat;

/// <summary>
/// Управляет пошаговой битвой между командой игрока и командой врагов.
/// Контролирует очередность ходов и логирует все события.
/// </summary>
/// <param name="players">Список героев под управлением игрока.</param>
/// <param name="enemies">Список вражеских героев.</param>
/// <param name="logger">Интерфейс для вывода текстовой информации о ходе боя.</param>
public class BattleManager(List<HeroBase> players, List<HeroBase> enemies, ICombatLogger logger)
{
    /// <summary>
    /// Возвращает <see langword="true"/>, если бой окончен. 
    /// Бой считается завершенным, когда все герои игрока или все враги мертвы.
    /// </summary>
    public bool IsBattleOver => players.TrueForAll(p => p.IsDead) || enemies.TrueForAll(e => e.IsDead);

    /// <summary>
    /// Выполняет ход игрока: выбранный атакующий наносит удар по выбранной цели.
    /// Если после удара бой не окончен, автоматически вызывается ответный ход противника.
    /// </summary>
    /// <param name="attacker">Герой игрока, совершающий атаку.</param>
    /// <param name="target">Вражеский герой, который получает урон.</param>
    public void ExecutePlayerTurn(HeroBase attacker, HeroBase target)
    {
        if (attacker.IsDead)
        {
            logger.Log("Мёртвые не могут атаковать!");
            return;
        }

        if (target.IsDead)
        {
            logger.Log("Эта цель уже мертва!");
            return;
        }

        int healthBefore = target.Health;
        attacker.Attack(target);
        int damageDealt = healthBefore - target.Health;

        logger.Log($"[{attacker.GetType().Name}] ударил [{target.GetType().Name}] на {damageDealt} урона. (У врага осталось {target.Health} HP)");

        if (!IsBattleOver)
        {
            ExecuteEnemyTurn();
        }
    }

    /// <summary>
    /// Выполняет автоматический ход всех живых врагов. 
    /// Каждый враг находит первую попавшуюся живую цель среди игроков и атакует её.
    /// </summary>
    private void ExecuteEnemyTurn()
    {
        logger.Log("--- Ход противника ---");

        foreach (var enemy in enemies.Where(e => e.IsAlive))
        {
            var target = players.FirstOrDefault(p => p.IsAlive);

            if (target is not null)
            {
                int healthBefore = target.Health;
                enemy.Attack(target);
                int damageDealt = healthBefore - target.Health;

                logger.Log($"Вражеский [{enemy.GetType().Name}] атаковал [{target.GetType().Name}] на {damageDealt} урона. (Осталось {target.Health} HP)");
            }
        }
        
        logger.Log("----------------------");
    }
}