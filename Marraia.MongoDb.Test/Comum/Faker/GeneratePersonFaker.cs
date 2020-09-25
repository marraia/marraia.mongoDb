using Bogus;
using Marraia.MongoDb.Test.Comum.Entities;
using System;
using System.Collections.Generic;

namespace Marraia.MongoDb.Test.Comum.Faker
{
    internal static class GeneratePersonFaker
    {
        public static PersonTest CreatePersonObjectFaker(Guid id)
        {
            var person = new Faker<PersonTest>("pt_BR")
               .RuleFor(c => c.Id, id)
               .RuleFor(c => c.Name, f => f.Name.FirstName())
               .RuleFor(c => c.Surname, f => f.Name.LastName())
               .Generate();

            return person;
        }

        public static IEnumerable<PersonTest> CreateListPersonObjectFaker(int countGenerate)
        {
            var person = new Faker<PersonTest>("pt_BR")
               .RuleFor(c => c.Id, f => f.Random.Guid())
               .RuleFor(c => c.Name, f => f.Name.FirstName())
               .RuleFor(c => c.Surname, f => f.Name.LastName())
               .Generate(countGenerate);

            return person;
        }
    }
}
