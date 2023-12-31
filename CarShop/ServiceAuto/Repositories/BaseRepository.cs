﻿using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceAuto.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : ModelEntity
    {
        protected readonly DbContext dbContext;
        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public T Add(T entity)
        {
            var added = dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
            return added.Entity;
        }

        public T Update(T entity)
        {
            var item = dbContext.Set<T>().Update(entity);
            dbContext.SaveChanges();
            return item.Entity;
        }

        public T Get(int id)
        {
            var item = dbContext.Set<T>()
                                .First(x => x.Id == id);
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>()
                            .ToList();
        }

        public void Remove(int entityId)
        {
            var element = dbContext.Set<T>()
                                   .First(e => e.Id == entityId);
            dbContext.Remove(element);
            dbContext.SaveChanges();
        }
    }
}
