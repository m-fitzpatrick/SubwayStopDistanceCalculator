using System;
using System.Collections.Generic;
using System.Linq;
using Coding.Assessment.Ipreo.Models.Entities.Interfaces;
using Coding.Assessment.Ipreo.Repositories.Interfaces;

namespace Coding.Assessment.Ipreo.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IEntityBase
    {
        protected IQueryable<T> DbSet { get; private set; }

        protected Repository()
        {
            LoadDbSet();
        }

        public IEnumerable<T> List()
        {
            return DbSet.ToList();
        }

        public T FirstOrDefault(Func<T, bool> where)
        {
            return DbSet.FirstOrDefault(where);
        }

        public IEnumerable<T> Where(Func<T, bool> where)
        {
            return DbSet.Where(where).ToList();
        }

        private void LoadDbSet()
        {
            DbSet = FetchDbSet();
        }

        protected abstract IQueryable<T> FetchDbSet();
    }
}