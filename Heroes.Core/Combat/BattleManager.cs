using System.Collections.Generic;
using System.Linq;
using Heroes.Core.Heroes;
using Heroes.Core.Interfaces;

namespace Heroes.Core.Combat;

/// <summary>
/// Управляет пошаговой битвой между командой игрока и врагами.
/// </summary>
public class BattleManager
{
    private List<HeroBase> _players;
    private List<HeroBase> _enemies;
    private ICombatLogger _logger;

    /// <summary>Бой окончен, если все игроки или все враги мертвы.</summary>
    public bool IsBattleOver => _players.TrueForAll(p => p.IsDead) || _enemies.TrueForAll(e => e.IsDead);

    private void ExecutePlayerTun(HeroBase attacker, HeroBase target)
    {
        if (attacker.IsDead)
        {
            _logger.Log("Мёртвые не могут атаковать!");
        }

        if (target.IsDead)
        {
            _logger.Log("Эта цель уже мертва!");
        }

        int healthBefore = target.Health;
        attacker.Attack(target);
        int damageDealt = healthBefore - target.Health;

        _logger.Log(
            $"[{attacker.GetType().Name}] ударил [{target.GetType().Name}] на {damageDealt} урона. (У врага осталось {target.Health} HP)");

        if (!IsBattleOver)
        {
            ExecuteEnemyTurn();
        }
    }

    /// <summary>
    /// Автоматический ход всех живых врагов по первому живому игроку.
    /// </summary>
    private void ExecuteEnemyTurn()
    {
        _logger.Log("--- Ход противника ---");

        foreach (var enemy in _enemies.Where(e => e.IsAlive))
        {
            var target = _players.FirstOrDefault(p => p.IsAlive);

            if (target is not null)
            {
                int healthBefore = target.Health;
                enemy.Attack(target);
                int damageDealt = healthBefore - target.Health;

                _logger.Log(
                    $"Вражеский [{enemy.GetType().Name}] атаковал [{target.GetType().Name}] на {damageDealt} урона. (Осталось {target.Health} HP)");
            }
        }

        _logger.Log("----------------------");
    }
}