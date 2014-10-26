// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the IRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        T Add(T entity);

        /// <summary>
        /// Takes the entity and stores it, adding it if necessary
        /// </summary>
        /// <returns>True if the entity is new, false if it already existed</returns>
        bool AddOrUpdate(T entity);

        void Delete(T entity);

        int GetCount(Expression<Func<T, bool>> where);
    }
}