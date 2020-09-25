using Bogus;
using FluentAssertions;
using Marraia.MongoDb.Test.Comum.Entities;
using System;
using Xunit;

namespace Marraia.MongoDb.Test.Entities
{
    public class EntityBaseTest
    {
        [Fact]
        public void ShouldEqualsTrueObject()
        {
            //arrange
            var faker = new Faker("pt_BR");
            var id = Guid.NewGuid();
            var name = faker.Person.FirstName;
            var surname = faker.Person.LastName;
            var obj1 = new PersonTest(id) { Name = name, Surname = surname };
            var obj2 = new PersonTest(id) { Name = name, Surname = surname };

            //act
            var equals = obj1.Equals(obj2);

            //assert
            equals
                .Should()
                .BeTrue();
        }

        [Fact]
        public void ShouldEqualsFalseObject()
        {
            //arrange
            var faker = new Faker("pt_BR");
            var name = faker.Person.FirstName;
            var surname = faker.Person.LastName;
            var obj1 = new PersonTest() { Name = name, Surname = surname };
            var obj2 = new PersonTest() { Name = name, Surname = surname };

            //act
            var equals = obj1.Equals(obj2);

            //assert
            equals
                .Should()
                .BeFalse();
        }


        [Fact]
        public void ShouldOperatorGetHashCodeObject()
        {
            //arrange
            var id = Guid.NewGuid();
            var faker = new Faker("pt_BR");
            var name = faker.Person.FirstName;
            var surname = faker.Person.LastName;
            var obj1 = new PersonTest(id) { Name = name, Surname = surname };
            var preview = obj1.GetType().GetHashCode() * 907 + id.GetHashCode();

            //act
            var hashCode = obj1.GetHashCode();

            //assert
            hashCode
                .Should()
                .Be(preview);
        }
    }
}
