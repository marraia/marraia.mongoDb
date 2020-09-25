using FluentAssertions;
using Marraia.MongoDb.Repositories;
using Marraia.MongoDb.Repositories.Interfaces;
using Marraia.MongoDb.Test.Comum.Entities;
using Marraia.MongoDb.Test.Comum.Faker;
using MongoDB.Driver;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Marraia.MongoDb.Test.Repositories
{
    public class MongoDbRepositoryBaseTest
    {
        private const int Received = 1;
        private IAsyncCursor<PersonTest> mongoCursor;
        private IMongoContext mongoContext;
        private MongoDbRepositoryBase<PersonTest, Guid> repositoryBase;

        public MongoDbRepositoryBaseTest()
        {
            mongoCursor = Substitute.For<IAsyncCursor<PersonTest>>();
            mongoContext = Substitute.For<IMongoContext>();
            repositoryBase = Substitute.For<MongoDbRepositoryBase<PersonTest, Guid>>(mongoContext);
        }

        [Fact]
        public async Task ShouldAbstractInsertAsyncWithSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var person = GeneratePersonFaker.CreatePersonObjectFaker(id);
            repositoryBase.When(x => x.InsertAsync(Arg.Any<PersonTest>())).CallBase();

            //act
            await repositoryBase
                    .InsertAsync(person)
                    .ConfigureAwait(false);

            //assert
            await repositoryBase
                    .Received(Received)
                    .InsertAsync(Arg.Any<PersonTest>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldAbstractUpdateAsyncWithSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var person = GeneratePersonFaker.CreatePersonObjectFaker(id);
            repositoryBase.When(x => x.UpdateAsync(Arg.Any<PersonTest>())).CallBase();

            //act
            await repositoryBase
                    .UpdateAsync(person)
                    .ConfigureAwait(false);

            //assert
            await repositoryBase
                    .Received(Received)
                    .UpdateAsync(Arg.Any<PersonTest>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldAbstractDeleteAsyncWithSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            repositoryBase.When(x => x.DeleteAsync(Arg.Any<Guid>())).CallBase();

            //act
            await repositoryBase
                    .DeleteAsync(id)
                    .ConfigureAwait(false);

            //assert
            await repositoryBase
                    .Received(Received)
                    .DeleteAsync(Arg.Any<Guid>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldAbstractGetByIdAsyncWithSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var person = GeneratePersonFaker.CreatePersonObjectFaker(id);
            var listPerson = new List<PersonTest>();
            listPerson.Add(person);

            mongoCursor.Current.Returns(listPerson);
            mongoCursor.MoveNext(Arg.Any<CancellationToken>()).Returns(true);
            mongoCursor.MoveNextAsync(Arg.Any<CancellationToken>()).Returns(true);

            repositoryBase.Collection.FindAsync(Arg.Any<FilterDefinition<PersonTest>>(),
                                                Arg.Any<FindOptions<PersonTest, PersonTest>>(),
                                                Arg.Any<CancellationToken>()).Returns(Task.FromResult(mongoCursor));


            repositoryBase
                .When(x => x.GetByIdAsync(id))
                .CallBase();

            //act
            var preview = await repositoryBase
                                    .GetByIdAsync(id)
                                    .ConfigureAwait(false);

            //assert
            preview
                .Should()
                .BeOfType<PersonTest>();

            preview.Id.Should().Equals(id);
            preview.Name.Should().Equals(person.Name);
            preview.Surname.Should().Equals(person.Surname);

            await repositoryBase
                    .Received(Received)
                    .GetByIdAsync(Arg.Any<Guid>())
                    .ConfigureAwait(false);
        }


        [Fact]
        public async Task ShouldAbstractGetByIdAsyncWithReturnDefault()
        {
            //arrange
            var id = Guid.NewGuid();
            var person = new PersonTest();

            mongoCursor.MoveNext(Arg.Any<CancellationToken>()).Returns(true);
            mongoCursor.MoveNextAsync(Arg.Any<CancellationToken>()).Returns(true);

            repositoryBase.Collection.FindAsync(Arg.Any<FilterDefinition<PersonTest>>(),
                                                Arg.Any<FindOptions<PersonTest, PersonTest>>(),
                                                Arg.Any<CancellationToken>()).Returns(Task.FromResult(mongoCursor));

            repositoryBase
                .GetByIdAsync(id)
                .Returns(Task.FromResult(person));

            repositoryBase
                .When(x => x.GetByIdAsync(Arg.Any<Guid>()))
                .CallBase();

            //act
            var preview = await repositoryBase
                                    .GetByIdAsync(id)
                                    .ConfigureAwait(false);

            //assert
            preview
                .Should()
                .BeOfType<PersonTest>();

            preview.Should().Be(person);

            await repositoryBase
                    .Received(Received)
                    .GetByIdAsync(Arg.Any<Guid>())
                    .ConfigureAwait(false);
        }
    }
}
