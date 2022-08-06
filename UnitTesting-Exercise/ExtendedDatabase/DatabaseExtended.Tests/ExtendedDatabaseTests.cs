namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Text;

    [TestFixture]
    public class DatabaseTests
    {
        private Database twoPeopleDatabase;

        private Database fullDatabase;

        [SetUp]
        public void SetUp()
        {
            Person[] twoPeopleList = new Person[2];
            for (int i = 0; i < 2; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Ivan");
                sb.Append(i.ToString());

                twoPeopleList[i] = new Person(1234 + i, sb.ToString()); 
            }

            twoPeopleDatabase = new Database(twoPeopleList);
            
            Person[] sixteenPeopleList = new Person[16];
            for (int i = 0; i < 16; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Ivan");
                sb.Append(i.ToString());

                sixteenPeopleList[i] = new Person(1234 + i, sb.ToString()); 
            }

            fullDatabase = new Database(sixteenPeopleList);
        }

        [Test]
        public void ConstructorShouldThrowExceptionWhenCollectionExceeds16()
        {
            Person[] twentyPeopleList = new Person[20];
            for (int i = 0; i < 16; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Ivan");
                sb.Append(i.ToString());

                twentyPeopleList[i] = new Person(1234 + i, sb.ToString());
            }

            Assert.That(() => new Database(twentyPeopleList), Throws.ArgumentException.With.Message.EqualTo("Provided data length should be in range [0..16]!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(16)]
        public void ConstructorShouldAddPeopleToDatabaseIfCollectionLessOrEqualTo16(int count)
        {
            Person[] peopleList = new Person[count];
            for (int i = 0; i < count; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Ivan");
                sb.Append(i.ToString());

                peopleList[i] = new Person(1234 + i, sb.ToString());
            }

            Database database = new Database(peopleList);

            Assert.That(database.Count, Is.EqualTo(count));
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenAddingExistingPersonById()
        {
            Person person = new Person(1235, "Ivan");

            Assert.That(() => twoPeopleDatabase.Add(person), Throws.InvalidOperationException.With.Message.EqualTo("There is already user with this Id!"));
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenAddingExistingPersonByName()
        {
            Person person = new Person(123, "Ivan1");

            Assert.That(() => twoPeopleDatabase.Add(person), Throws.InvalidOperationException.With.Message.EqualTo("There is already user with this username!"));
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenPeopleExceed16()
        {
            Person person = new Person(123, "Ivan");

            Assert.That(() => fullDatabase.Add(person), Throws.InvalidOperationException.With.Message.EqualTo("Array's capacity must be exactly 16 integers!"));
        }

        [Test]
        public void RemoveMethodShouldThrowExceptionIfDatabaseIsEmpty()
        {
            Database db = new Database();

            Assert.Throws<InvalidOperationException>(() => db.Remove());
        }

        [Test]
        public void RemoveMethodShouldSuccessfullyRemovePeople()
        {
            fullDatabase.Remove();
            fullDatabase.Remove();

            Assert.That(fullDatabase.Count, Is.EqualTo(14));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void FindByUsernameMethodShouldThrowExceptionWhenInputIsInvalid(string input)
        {
            Assert.Throws<ArgumentNullException>(() => fullDatabase.FindByUsername(input));
        }

        [Test]
        public void FindByUsernameMethodShouldThrowExceptionWhenUserDoNotExist()
        {
            Assert.That(() => fullDatabase.FindByUsername("Petar"), Throws.InvalidOperationException.With.Message.EqualTo("No user is present by this username!"));
        }

        [Test]
        public void FindByUsernameMethodShouldReturnPersonWithThatUsername()
        {
            Person result = fullDatabase.FindByUsername("Ivan0");
            Person expectedPerson = new Person(1234, "Ivan0");

            Assert.AreEqual((expectedPerson.UserName, expectedPerson.Id), (result.UserName, result.Id));
        }

        [Test]
        public void FindByIdMethodShouldThrowExceptionWhenNoUserInDatabase()
        {
            Assert.That(() => fullDatabase.FindById(1), Throws.InvalidOperationException.With.Message.EqualTo("No user is present by this ID!"));
        }

        [Test]
        public void FindByIdMethodShouldThrowExceptionWhenNegativeIdIsProvided()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => fullDatabase.FindById(-1234));
        }

        [Test]
        public void FindByIdMethodShouldReturnPersonWithProvidedId()
        {
            Person result = fullDatabase.FindById(1234);
            Person expectedPerson = new Person(1234, "Ivan0");

            Assert.AreEqual((expectedPerson.UserName, expectedPerson.Id), (result.UserName, result.Id));
        }
    }
}