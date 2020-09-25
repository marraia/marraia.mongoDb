using FluentAssertions;
using Marraia.MongoDb.Test.Comum.Entities;
using Marraia.MongoDb.Test.Comum.Faker;
using Marraia.MongoDb.Test.Comum.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Marraia.MongoDb.Test.Repositories
{
    public class PersonRepositoryTest
    {
        private IPersonRepository personRepository;
        private const int CountGenerator = 10;
        private const int CountNoRecords = 0;
        private const int Received = 1;
        public PersonRepositoryTest()
        {
            personRepository = Substitute.For<IPersonRepository>();
        }

        [Fact]
        public async Task ShouldInsertAsyncWithSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var person = GeneratePersonFaker.CreatePersonObjectFaker(id);

            //act
            await personRepository
                    .InsertAsync(person)
                    .ConfigureAwait(false);

            //assert
            await personRepository
                    .Received(Received)
                    .InsertAsync(Arg.Any<PersonTest>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldUpdateAsyncWithSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var person = GeneratePersonFaker.CreatePersonObjectFaker(id);

            //act
            await personRepository
                    .UpdateAsync(person)
                    .ConfigureAwait(false);

            //assert
            await personRepository
                    .Received(Received)
                    .UpdateAsync(Arg.Any<PersonTest>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldDeleteAsyncWithSuccess()
        {
            //arrange
            var id = Guid.NewGuid();

            //act
            await personRepository
                    .DeleteAsync(id)
                    .ConfigureAwait(false);

            //assert
            await personRepository
                    .Received(Received)
                    .DeleteAsync(Arg.Any<Guid>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldGetByIdAsyncWithSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var person = GeneratePersonFaker.CreatePersonObjectFaker(id);

            personRepository
                .GetByIdAsync(id)
                .Returns(Task.FromResult(person));

            //act
            var preview = await personRepository
                                    .GetByIdAsync(id)
                                    .ConfigureAwait(false);

            //assert
            preview
                .Should()
                .BeOfType<PersonTest>();

            preview.Id.Should().Equals(id);
            preview.Name.Should().Equals(person.Name);
            preview.Surname.Should().Equals(person.Surname);

            await personRepository
                    .Received(Received)
                    .GetByIdAsync(Arg.Any<Guid>())
                    .ConfigureAwait(false);
        }


        [Fact]
        public async Task ShouldGetByIdAsyncWithReturnDefault()
        {
            //arrange
            var id = Guid.NewGuid();
            var person = new PersonTest();

            personRepository
                .GetByIdAsync(id)
                .Returns(Task.FromResult(person));

            //act
            var preview = await personRepository
                                    .GetByIdAsync(id)
                                    .ConfigureAwait(false);

            //assert
            preview
                .Should()
                .BeOfType<PersonTest>();

            preview.Should().Be(person);

            await personRepository
                    .Received(Received)
                    .GetByIdAsync(Arg.Any<Guid>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldGetAllAsyncWithSuccess()
        {
            //arrange
            var persons = GeneratePersonFaker.CreateListPersonObjectFaker(CountGenerator);

            personRepository
                .GetAllAsync()
                .Returns(Task.FromResult(persons));

            //act
            var preview = await personRepository
                                    .GetAllAsync()
                                    .ConfigureAwait(false);

            //assert
            preview
                .Should()
                .HaveCount(CountGenerator);

            await personRepository
                    .Received(Received)
                    .GetAllAsync()
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldGetAllAsyncNoRecords()
        {
            //arrange
            IEnumerable<PersonTest> persons = new List<PersonTest>();

            personRepository
                .GetAllAsync()
                .Returns(Task.FromResult(persons));

            //act
            var preview = await personRepository
                                    .GetAllAsync()
                                    .ConfigureAwait(false);

            //assert
            preview
                .Should()
                .HaveCount(CountNoRecords);

            await personRepository
                    .Received(Received)
                    .GetAllAsync()
                    .ConfigureAwait(false);
        }
    }
}
