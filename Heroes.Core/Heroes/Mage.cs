using Heroes.Core.Interfaces;


namespace Heroes.Core.Heroes;

public class Mage : HeroBase, IAttacker
{
    public override void Attack(HeroBase enemy)
    {
        MagicAttack(enemy);
    }

    public void MagicAttack(HeroBase enemy)
    {
        throw new System.NotImplementedException();
    }
}