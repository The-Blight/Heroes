using Heroes.Core.UI;

namespace Heroes.Core;
using SadConsole;
using SadConsole.Configuration;

internal class Program
{
    static void Main(string[] args)
    {
        Settings.WindowTitle = "Heroes ASCII Battle";

        Builder configuration = new Builder()
            .SetScreenSize(80, 25) 
            .OnStart(Startup);     

        SadConsole.Game.Create(configuration);
        SadConsole.Game.Instance.Run();
        SadConsole.Game.Instance.Dispose();
    }

    private static void Startup(object? sender, GameHost host)
    {
        var battleScreen = new BattleScreen();

        SadConsole.Game.Instance.Screen = battleScreen;
        SadConsole.Game.Instance.DestroyDefaultStartingConsole();
    }
}