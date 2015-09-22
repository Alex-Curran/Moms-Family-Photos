using System;
using System.Collections;
using PhotoStorage.Models;

namespace PhotoStorage.DAL
{
    public interface IGenericRepository<T> : IDisposable
    {
        T GetById(int id);
        void Add(T newEntity);
        void Delete(T entity);
        void Update(T entityToUpdate, int id);
        void Save();
    }
}