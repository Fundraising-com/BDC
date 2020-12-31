using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Data.Common;

namespace Business
{
    /// <summary>
    /// Repository Interface defines the base
    /// functionality required by all Repositories.
    /// </summary>
    /// <typeparam name="T">
    /// The entity type that requires a Repository.
    /// </typeparam>
    public interface IRepository<E>
    {
     //   DbTransaction BeginTransaction();
        void Add(E entity);
        void AddOrAttach(E entity);
        void DeleteRelatedEntries(E entity);
        void DeleteRelatedEntries
        (E entity, ObservableCollection<string> keyListOfIgnoreEntites);
        void Delete(E entity);
        int Save();

        ObjectQuery<E> DoQuery(string entitySetName);
        ObjectQuery<E> DoQuery();
        ObjectQuery<E> DoQuery(string entitySetName, ISpecification<E> where);
        ObjectQuery<E> DoQuery(ISpecification<E> where);
        ObjectQuery<E> DoQuery(int maximumRows, int startRowIndex);
        ObjectQuery<E> DoQuery(Expression<Func<E, object>> sortExpression);
        ObjectQuery<E> DoQuery(Expression<Func<E, object>> sortExpression,
                    int maximumRows, int startRowIndex);

        IList<E> SelectAll(string entitySetName);
        IList<E> SelectAll();
        IList<E> SelectAll(string entitySetName, ISpecification<E> where);
        IList<E> SelectAll(ISpecification<E> where);
        IList<E> SelectAll(int maximumRows, int startRowIndex);
        IList<E> SelectAll(Expression<Func<E, object>> sortExpression);
        IList<E> SelectAll(Expression<Func<E, object>> sortExpression,
                    int maximumRows, int startRowIndex);

        E SelectByKey(string Key);

        bool TrySameValueExist(string fieldName, object fieldValue, string key);
        bool TryEntity(ISpecification<E> selectSpec);

        int GetCount();
        int GetCount(ISpecification<E> selectSpec);

     //   void AddToT<T>(ref T o);
     //   void AttachToT<T>(ref T o);

    }
}