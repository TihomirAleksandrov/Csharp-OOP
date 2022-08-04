using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyLoosesHealthIfAttacked()
        {
            Dummy dummy = new Dummy(10, 10);
            dummy.TakeAttack(5);
            
            Assert.AreEqual(5, dummy.Health);
        }

        [Test]
        public void DeadDummyThrowsExceptionIfAttacked()
        {
            Dummy dummy = new Dummy(0, 10);

            Assert.That(() => dummy.TakeAttack(5), Throws.InvalidOperationException.With.Message.EqualTo("Dummy is dead."));
        }

        [Test]
        public void DeadDummyCanGiveXP()
        {
            Dummy dummy = new Dummy(0, 10);

            Assert.AreEqual(10, dummy.GiveExperience());
        }

        [Test]
        public void AliveDummyCanNotGiveXP()
        {
            Dummy dummy = new Dummy(10, 10);

            Assert.That(() => dummy.GiveExperience(), Throws.InvalidOperationException.With.Message.EqualTo("Target is not dead."));
        }
}
}