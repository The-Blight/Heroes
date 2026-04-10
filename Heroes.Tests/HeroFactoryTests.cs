using Heroes.Core.Factory;
using Heroes.Core.Heroes;
using Heroes.Core.Interfaces;

namespace Heroes.Tests;

public class HeroFactoryTests
{
    [Fact]
    public void Create_RegisteredHero_ReturnsCorrectInstanceAndStats()
    {
        var factory = new HeroFactory();
        factory.RegisterType<Mage>((h, d) =>
        {
            var mage1 = new Mage { BaseDamage = d };
            mage1.Health = h;
            return mage1;
        });


        var mage = factory.Create<Mage>(health: 80, damage: 25);


        Assert.Multiple(
            () => Assert.NotNull(mage),
            () => Assert.Equal(80, mage.Health),
            () => Assert.Equal(25, mage.BaseDamage)
        );
    }


    [Fact]
    public void Create_UnregisteredHero_ThrowsInvalidOperationException()
    {
        var factory = new HeroFactory();


        Assert.Throws<InvalidOperationException>(() => factory.Create<Warrior>(100, 10));
    }
}

public class HeroCombatTests
{
    
    private class StubTarget : IDamageable
    {
        public int Health { get; private set; } = 100;
        public bool IsAlive => Health > 0;
        public void TakeDamage(int amount) => Health -= amount;
    }

    [Fact]
    public void TakeDamage_ValidAmount_ReducesHealth()
    {
        var mage = new Mage { Health = 100, BaseDamage = 10 };


        mage.TakeDamage(30);


        Assert.Equal(70, mage.Health);
        Assert.True(mage.IsAlive);
    }

    [Fact]
    public void TakeDamage_FatalDamage_HealthDoesNotDropBelowZero()
    {
        var warrior = new Warrior { Health = 50, BaseDamage = 15 };


        warrior.TakeDamage(100);

        Assert.Multiple(
            () => Assert.Equal(0, warrior.Health),
            () => Assert.False(warrior.IsAlive),
            () => Assert.True(warrior.IsDead)
        );
    }

    [Fact]
    public void Attack_ValidTarget_ReducesTargetHealth()
    {
        var warrior = new Warrior { Health = 100, BaseDamage = 25 };
        var target = new StubTarget();


        warrior.Attack(target);


        Assert.Equal(75, target.Health);
    }

    [Fact]
    public void Attack_DeadAttacker_DoesNotReduceTargetHealth()
    {
        var deadMage = new Mage { Health = 0, BaseDamage = 50 };
        var target = new StubTarget();


        deadMage.Attack(target);


        Assert.Equal(100, target.Health);
    }
}