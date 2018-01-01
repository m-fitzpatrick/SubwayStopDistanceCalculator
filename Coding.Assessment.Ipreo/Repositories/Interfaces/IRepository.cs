using System;
using System.Collections.Generic;
using Coding.Assessment.Ipreo.Models.Entities.Interfaces;

namespace Coding.Assessment.Ipreo.Repositories.Interfaces
{
    public interface IRepository<out T> where T : IEntityBase
    {
        T FirstOrDefault(Func<T, bool> where);

        IEnumerable<T> Where(Func<T, bool> where);

        IEnumerable<T> List();
    }
}