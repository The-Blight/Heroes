using Heroes.Core.Interfaces;


namespace Heroes.Core.Heroes;

public class Warrior : HeroBase, IAttacker
{
    public override void Attack(HeroBase enemy)
    {
        PhysicalAttack(enemy);
    }

    public void PhysicalAttack(HeroBase enemy)
    {
        throw new System.NotImplementedException();
    }
}