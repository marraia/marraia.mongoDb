using Marraia.MongoDb.Repositories;
using Marraia.MongoDb.Repositories.Interfaces;
using Marraia.MongoDb.Test.Comum.Entities;
using Marraia.MongoDb.Test.Comum.Interfaces;
using System;

namespace Marraia.MongoDb.Test.Comum
{
    public class PersonRepository : MongoDbRepositoryBase<PersonTest, Guid>, IPersonRepository
    {
        public PersonRepository(IMongoContext context)
            : base(context)
        {
        }
    }
}
