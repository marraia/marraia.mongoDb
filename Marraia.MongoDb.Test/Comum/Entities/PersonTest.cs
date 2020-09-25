using Marraia.MongoDb.Core;
using System;

namespace Marraia.MongoDb.Test.Comum.Entities
{
    public class PersonTest
            : Entity<Guid>
    {
        public PersonTest()
        {
            Id = Guid.NewGuid();
        }
        public PersonTest(Guid id)
        {
            Id = id;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
