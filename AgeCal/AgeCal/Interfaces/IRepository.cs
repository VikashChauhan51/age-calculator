﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface IRepository<TEntity, in TKey>
    {
        /// <summary>
        /// get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(TKey id);
        /// <summary>
        /// add an entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        /// <summary>
        /// delete an entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// serch entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, int skip, int take);
        IEnumerable<TEntity> GetAll( int skip, int take);
    }
}
