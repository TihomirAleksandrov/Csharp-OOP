using NUnit.Framework;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void Axe_LossesDurability_AfterAttack()
        {
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(5, 5);

            axe.Attack(dummy);

            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe durability doesn't change after attack.");
        }
    }
}