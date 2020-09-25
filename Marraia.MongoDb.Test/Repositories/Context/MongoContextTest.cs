using FluentAssertions;
using Marraia.MongoDb.Repositories.Context;
using Marraia.MongoDb.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NSubstitute;
using System;
using Xunit;

namespace Marraia.MongoDb.Test.Repositories.Context
{
    public class MongoContextTest
    {
        private IMongoContext mongoContext;
        public MongoContextTest()
        {
            mongoContext = Substitute.For<IMongoContext>();
        }

        [Fact]
        public void ShouldMongoContextInjectionWithSuccess()
        {
            //arrange
            var configuration = new ConfigurationBuilder()
                             .AddJsonFile("appsettings.json")
                             .Build();
            //act
            mongoContext = new MongoContext(configuration);

            //assert
            mongoContext
                .Client
                .Should()
                .NotBeNull();

            mongoContext
                .Database
                .DatabaseNamespace
                .DatabaseName
                .Should()
                .Be(configuration["MongoSettings:Database"]);
        }

        [Fact]
        public void ShouldMongoContextErrorInjectionWithoutInformingSettings()
        {
            //arrange
            var configuration = new ConfigurationBuilder()
                                    .Build();
            //act
            Action action = () => mongoContext = new MongoContext(configuration);

            //assert
            action
                .Should()
                .Throw<MongoClientException>()
                .WithMessage("Informe em seu arquivo de configuração, o servidor e database MongoDb");
        }
    }
}
