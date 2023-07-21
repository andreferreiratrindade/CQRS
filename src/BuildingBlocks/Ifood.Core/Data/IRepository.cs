using System;
using System.Data.Common;
using Ifood.Core.DomainObjects;

namespace Ifood.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        DbConnection GetConnection();

    }
}