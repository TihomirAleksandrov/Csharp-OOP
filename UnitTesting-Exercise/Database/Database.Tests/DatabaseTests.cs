namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;
        
        [SetUp]
        public void SetUp()
        {
            this.database = new Database();
        }
        
        [Test]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(16)]
        public void AddMethodCanAddWhileCountIsLessThan16(int count)
        {
            for (int i = 0; i < count; i++)
            {
                database.Add(i);
            }

            Assert.AreEqual(count, database.Count);
        }

        [Test]
        public void AddMethodShouldThrowAnExceptionWhenAddingMoreThan16Items()
        {
            int[] numbers = Enumerable.Range(1, 16).ToArray();
            this.database = new Database(numbers);

            Assert.Throws<InvalidOperationException>(() => database.Add(1));
        }

        [Test]
        [TestCase(1,4)]
        [TestCase(1,15)]
        [TestCase(1,16)]
        public void ConstructorShouldAddElementsWhileLessThan16(int start, int end)
        {
            var elements = Enumerable.Range(start, end).ToArray();
            this.database = new Database(elements);

            Assert.AreEqual(end, database.Count);
        }

        [Test]
        public void ConstructorShouldThrowExceptionIfElementsMoreThan16()
        {
            var elements = Enumerable.Range(1, 17).ToArray();

            Assert.Throws<InvalidOperationException>(() => new Database(elements));
        }

        [Test]
        [TestCase(16)]
        [TestCase(1)]
        public void RemoveMethodShouldRemoveItemWhileCountIsMoreThan0(int range)
        {
            var elements = Enumerable.Range(1, range).ToArray();
            this.database = new Database(elements);

            database.Remove();

            Assert.AreEqual(range - 1, database.Count);
        }

        [Test]
        public void RemoveMethodShouldThrowExceptionWhenCountIs0()
        {
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void FetchMethodShouldReturnValidItems()
        {
            database.Add(1);
            database.Add(2);
            database.Add(3);
            database.Add(4);

            database.Remove();

            int[] fetchedDb = database.Fetch();
            int[] expectedData = new int[] { 1, 2, 3 };

            CollectionAssert.AreEqual(expectedData, fetchedDb);
        }
    }
}
