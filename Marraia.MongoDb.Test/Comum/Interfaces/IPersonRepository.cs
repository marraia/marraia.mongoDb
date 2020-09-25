using Marraia.MongoDb.Repositories.Interfaces;
using Marraia.MongoDb.Test.Comum.Entities;
using System;

namespace Marraia.MongoDb.Test.Comum.Interfaces
{
    public interface IPersonRepository : IRepositoryBase<PersonTest, Guid>
    {
    }
}
