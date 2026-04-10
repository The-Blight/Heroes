using System.Collections.Generic;
using SadConsole;
using SadConsole.Input;
using Heroes.Core.Combat;
using Heroes.Core.Factory;
using Heroes.Core.Heroes;
using Heroes.Core.Interfaces;
using Heroes.Core.Weapons;

namespace Heroes.Core.UI;

public class BattleScreen : Console, ICombatLogger
{
    private readonly BattleManager _battleManager;
    private readonly List<HeroBase> _players;
    private readonly List<HeroBase> _enemies;

    public BattleScreen() : base(80, 25)
    {
        UseKeyboard = true;
        IsFocused = true;   
        Cursor.IsVisible = false;

        var factory = new HeroFactory();
        factory.RegisterType<Warrior>((h, d) => new Warrior { Health = h, BaseDamage = d });
        factory.RegisterType<Mage>((h, d) => new Mage { Health = h, BaseDamage = d });

        var player = factory.Create<Warrior>(100, 10);
        player.EquipWeapon(new Sword()); 
        
        var enemy = factory.Create<Mage>(80, 15);
        enemy.EquipWeapon(new Staff());

        _players = [player];
        _enemies = [enemy];

        _battleManager = new BattleManager(_players, _enemies, this);

        Log("====== БОЙ НАЧАЛСЯ ======");
        Log("Нажмите [ПРОБЕЛ] (Space) для атаки.");
        Log("");
    }

    public void Log(string message)
    {
        Cursor.Print(message + "\r\n");
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        if (keyboard.IsKeyPressed(Keys.Space))
        {
            if (!_battleManager.IsBattleOver)
            {
                _battleManager.ExecutePlayerTurn(_players[0], _enemies[0]);
            }
            else
            {
                Log("Бой уже окончен! Нажмите [ESC] для выхода.");
            }
            return true; 
        }

        if (keyboard.IsKeyPressed(Keys.Escape))
        {
            System.Environment.Exit(0); 
            return true;
        }

        return base.ProcessKeyboard(keyboard);
    }
}