using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.ServiceLocation;
using System.Data.Metadata.Edm;


namespace Business
{
    public class EFRepository<E> : IRepository<E>, IDisposable
        where E : class
    {
        public readonly ObjectContext _ctx;

        private string _KeyProperty = "ID";

        private EntityKey key=null;
 

        public string KeyProperty
        {
            get
            {
                return _KeyProperty;
            }
            set
            {
                _KeyProperty = value;
            }
        }

        public ObjectContext Session
        {
            get { return _ctx; }
        }

        public EFRepository()
        {

            _ctx = ServiceLocator.Current.GetInstance<ObjectContext>();
            
        }

        #region IRepository<E,C> Members

        public int Save()
        {
            return _ctx.SaveChanges();
        }
        /// <summary>
        /// A generic method to return ALL the entities
        /// </summary>
        /// <param name=”entitySetName”>
        /// The EntitySet name of the entity in the model.
        /// </param>
        /// <typeparam name=”TEntity”>
        /// The Entity to load from the database.
        /// </typeparam>
        /// <returns>Returns a set of TEntity.</returns>
        public ObjectQuery<E> DoQuery(string entitySetName)
        {
            return _ctx.CreateQuery<E>("[" + entitySetName + "]");
        }
        /// <summary>
        /// A generic method to return ALL the entities
        /// </summary>
        /// <typeparam name=”TEntity”>
        /// The Entity to load from the database.
        /// </typeparam>
        /// <returns>Returns a set of TEntity.</returns>
        public ObjectQuery<E> DoQuery()
        {
            return _ctx.CreateQuery<E>("[" + typeof(E).Name + "s]");
        }

        /// <summary>
        /// </summary>
        /// <param name=”entitySetName”>
        /// The EntitySet name of the entity in the model.
        /// </param>
        /// <typeparam name=”TEntity”>
        /// The Entity to load from the database.
        /// </typeparam>
        /// <returns>Returns a set of TEntity.</returns>
        public ObjectQuery<E> DoQuery(string entitySetName, ISpecification<E> where)
        {

            return
                (ObjectQuery<E>)_ctx.CreateQuery<E>("[" + entitySetName + "s]")
                .Where(where.EvalPredicate);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name=”TEntity”>
        /// The Entity to load from the database.
        /// </typeparam>
        /// <returns>Returns a set of TEntity.</returns>
        public ObjectQuery<E> DoQuery(ISpecification<E> where)
        {
            string entitySetName = _ctx.MetadataWorkspace
                        .GetEntityContainer(_ctx.DefaultContainerName, DataSpace.CSpace)
                        .BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(E).Name).First().Name;

            string name = String.Format("{0}.{1}", _ctx.DefaultContainerName, entitySetName);

            return
                (ObjectQuery<E>)_ctx.CreateQuery<E>(name)
                .Where(where.EvalPredicate);
        }
        /// <summary>
        /// Query Entity with Paging 
        /// </summary>
        /// <param name="maximumRows">Max no of row to Fetch</param>
        /// <param name="startRowIndex">Start Index</param>
        /// <returns>Collection of Entities</returns>
        public ObjectQuery<E> DoQuery(int maximumRows, int startRowIndex)
        {
            return (ObjectQuery<E>)_ctx.CreateQuery<E>
        ("[" + typeof(E).Name + "]").Skip<E>(startRowIndex).Take(maximumRows);
        }
        /// <summary>
        /// Query Entity in sorted Order
        /// </summary>
        /// <param name="sortExpression">Sort Expression/condition</param>
        /// <param name="ErrorCode">custom Error Message</param> 
        /// <returns>Collection of Entities</returns>
        public ObjectQuery<E> DoQuery(Expression<Func<E, object>> sortExpression)
        {
            if (null == sortExpression)
            {
                return ((IRepository<E>)this).DoQuery();
            }
            return (ObjectQuery<E>)((IRepository<E>)this).DoQuery().OrderBy
                        <E, object>(sortExpression);
        }
        /// <summary>
        /// Query All Entity in sorted Order with Paging support
        /// </summary>
        /// <param name="sortExpression">Sort Expression/condition</param>
        /// <param name="maximumRows">Max no of row to Fetch</param>
        /// <param name="startRowIndex">Start Index</param>
        /// <returns>Collection Of entities</returns>
        public ObjectQuery<E> DoQuery(Expression<Func<E, object>>
            sortExpression, int maximumRows, int startRowIndex)
        {
            if (sortExpression == null)
            {
                return ((IRepository<E>)this).DoQuery(maximumRows, startRowIndex);
            }
            return (ObjectQuery<E>)((IRepository<E>)this).DoQuery
            (sortExpression).Skip<E>(startRowIndex).Take(maximumRows);
        }
        /// <summary>
        /// A generic method to return ALL the entities
        /// </summary>
        /// <param name=”entitySetName”>
        /// The EntitySet name of the entity in the model.
        /// </param>
        /// <typeparam name=”TEntity”>
        /// The Entity to load from the database.
        /// </typeparam>
        /// <returns>Returns a set of TEntity.</returns>
        public IList<E> SelectAll(string entitySetName)
        {
            return DoQuery(entitySetName).ToList();
        }
        /// <summary>
        /// A generic method to return ALL the entities
        /// </summary>
        /// <typeparam name=”TEntity”>
        /// The Entity to load from the database.
        /// </typeparam>
        /// <returns>Returns a set of TEntity.</returns>
        public IList<E> SelectAll()
        {
            try
            {
                return DoQuery().ToList(); //_ctx.CreateQuery<E>
                //("[" + typeof(E).Name + "]");
            }
            catch (EntitySqlException sss)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// A generic method to return ALL the entities
        /// </summary>
        /// <param name=”entitySetName”>
        /// The EntitySet name of the entity in the model.
        /// </param>
        /// <typeparam name=”TEntity”>
        /// The Entity to load from the database.
        /// </typeparam>
        /// <returns>Returns a set of TEntity.</returns>
        public IList<E> SelectAll(string entitySetName, ISpecification<E> where)
        {
            return DoQuery(entitySetName, where).ToList();
        }

        /// <summary>
        /// A generic method to return ALL the entities
        /// </summary>
        /// <typeparam name=”TEntity”>
        /// The Entity to load from the database.
        /// </typeparam>
        /// <returns>Returns a set of TEntity.</returns>
        public IList<E> SelectAll(ISpecification<E> where)
        {
            return DoQuery(where).ToList();
        }
        /// <summary>
        /// Select All Entity with Paging 
        /// </summary>
        /// <param name="maximumRows">Max no of row to Fetch</param>
        /// <param name="startRowIndex">Start Index</param>
        /// <returns>Collection of Entities</returns>
        public IList<E> SelectAll(int maximumRows, int startRowIndex)
        {
            return DoQuery(maximumRows, startRowIndex).ToList();
        }
        /// <summary>
        /// Select All Entity in sorted Order
        /// </summary>
        /// <param name="sortExpression">Sort Expression/condition</param>
        /// <param name="ErrorCode">custom Error Message</param> 
        /// <returns>Collection of Entities</returns>
        public IList<E> SelectAll(Expression<Func<E, object>> sortExpression)
        {
            if (null == sortExpression)
            {
                return DoQuery(sortExpression).ToList();
            }
            return DoQuery(sortExpression).ToList();
        }
        /// <summary>
        /// Select All Entity in sorted Order with Paging support
        /// </summary>
        /// <param name="sortExpression">Sort Expression/condition</param>
        /// <param name="maximumRows">Max no of row to Fetch</param>
        /// <param name="startRowIndex">Start Index</param>
        /// <returns>Collection Of entities</returns>
        public IList<E> SelectAll(Expression<Func<E, object>>
            sortExpression, int maximumRows, int startRowIndex)
        {
            if (sortExpression == null)
            {
                return DoQuery(maximumRows, startRowIndex).ToList();
            }
            return DoQuery(sortExpression, maximumRows, startRowIndex).ToList();
        }
        /// <summary>
        /// Get Entity By Primary Key
        /// </summary>
        /// <typeparam name="E">Entity Type</typeparam>
        /// <param name="Key">Primary Key Value</param>
        /// <returns>return entity</returns>
        public E SelectByKey(string Key)
        {
            // First we define the parameter that we are going to use the clause. 
            var xParam = Expression.Parameter(typeof(E), typeof(E).Name);
            MemberExpression leftExpr = MemberExpression.Property(xParam, this._KeyProperty);
            Expression rightExpr = Expression.Constant(Key);
            BinaryExpression binaryExpr = MemberExpression.Equal(leftExpr, rightExpr);
            //Create Lambda Expression for the selection 
            Expression<Func<E, bool>> lambdaExpr =
            Expression.Lambda<Func<E, bool>>(binaryExpr,
            new ParameterExpression[] { xParam });
            //Searching ....
            IList<E> resultCollection = ((IRepository<E>)this).SelectAll
                        (new Specification<E>(lambdaExpr));
            if (null != resultCollection && resultCollection.Count() > 0)
            {
                //return valid single result
                return resultCollection.First<E>();
            }//end if 
            return default(E);
        }
        /// <summary>
        /// Check if value of specific field is already exist
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="fieldName">name of the Field</param>
        /// <param name="fieldValue">Field value</param>
        /// <param name="key">Primary key value</param>
        /// <returns>True or False</returns>
        public bool TrySameValueExist(string fieldName, object fieldValue, string key)
        {
            // First we define the parameter that we are going to use the clause. 
            var xParam = Expression.Parameter(typeof(E), typeof(E).Name);
            MemberExpression leftExprFieldCheck =
            MemberExpression.Property(xParam, fieldName);
            Expression rightExprFieldCheck = Expression.Constant(fieldValue);
            BinaryExpression binaryExprFieldCheck =
            MemberExpression.Equal(leftExprFieldCheck, rightExprFieldCheck);

            MemberExpression leftExprKeyCheck =
            MemberExpression.Property(xParam, this._KeyProperty);
            Expression rightExprKeyCheck = Expression.Constant(key);
            BinaryExpression binaryExprKeyCheck =
            MemberExpression.NotEqual(leftExprKeyCheck, rightExprKeyCheck);
            BinaryExpression finalBinaryExpr =
            Expression.And(binaryExprFieldCheck, binaryExprKeyCheck);

            //Create Lambda Expression for the selection 
            Expression<Func<E, bool>> lambdaExpr =
            Expression.Lambda<Func<E, bool>>(finalBinaryExpr,
            new ParameterExpression[] { xParam });
            //Searching ....            
            return ((IRepository<E>)this).TryEntity(new Specification<E>(lambdaExpr));
        }
        /// <summary>
        /// Check if Entities exist with Condition
        /// </summary>
        /// <param name="selectExpression">Selection Condition</param>
        /// <returns>True or False</returns>
        public bool TryEntity(ISpecification<E> selectSpec)
        {
            return _ctx.CreateQuery<E>("[" + typeof(E).Name + "]").Any<E>
                        (selectSpec.EvalPredicate);
        }
        /// <summary>
        /// Get Count of all records
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <returns>count of all records</returns>
        public int GetCount()
        {
            return _ctx.CreateQuery<E>("[" + typeof(E).Name + "]").Count();
        }
        /// <summary>
        /// Get count of selection
        /// </summary>
        /// <typeparam name="E">Selection Condition</typeparam>
        /// <returns>count of selection</returns>
        public int GetCount(ISpecification<E> selectSpec)
        {
            return _ctx.CreateQuery<E>("[" + typeof(E).Name + "]")
                .Where(selectSpec.EvalPredicate).Count();
        }
        /// <summary>
        /// Delete data from context
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        public void Delete(E entity)
        {
            _ctx.DeleteObject(entity);
        }
        /// <summary>
        /// Delete data from context
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        public void Delete(object entity)
        {
            _ctx.DeleteObject(entity);
        }
        /// <summary>
        /// Insert new data into context
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        public void Add(E entity)
        {
            string entitySetName = _ctx.MetadataWorkspace
                                    .GetEntityContainer(_ctx.DefaultContainerName, DataSpace.CSpace)
                                    .BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(E).Name).First().Name;

            string name = String.Format("{0}.{1}", _ctx.DefaultContainerName, entitySetName);

            _ctx.AddObject(name, entity);
        }
        /// <summary>
        /// Insert if new otherwise attach data into context
        /// </summary>
        /// <param name="entity"></param>
        public void AddOrAttach(E entity)
        {
            // Define an ObjectStateEntry and EntityKey for the current object.
            object originalItem;
            // Get the detached object's entity key.
            if (key == null)
            {
                // Get the entity key of the updated object.
                key = _ctx.CreateEntityKey(entity.GetType().Name, entity);
            }
           
            try
            {
                // Get the original item based on the entity key from the context
                // or from the database.
                if (_ctx.TryGetObjectByKey(key, out originalItem))
                {//accept the changed property
                    if (originalItem is EntityObject &&
                        ((EntityObject)originalItem).EntityState != EntityState.Added)
                    {
                        // Call the ApplyPropertyChanges method to apply changes
                        // from the updated item to the original version.
                        _ctx.ApplyPropertyChanges(
                            key.EntitySetName, entity);
                    }
                }
                else
                {//add the new entity
                    Add(entity);
                }//end else
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Start Transaction
        /// </summary>
        /// <returns></returns>
      /*  public DbTransaction BeginTransaction()
        {
            if (_ctx.Connection.State != ConnectionState.Open)
            {
                _ctx.Connection.Open();
            }
            return _ctx.Connection.BeginTransaction();
        }
        */
        /// <summary>
        /// Delete all related entries
        /// </summary>
        /// <param name="entity"></param>        
        public void DeleteRelatedEntries(E entity)
        {
            foreach (var relatedEntity in (((IEntityWithRelationships)entity).
        RelationshipManager.GetAllRelatedEnds().SelectMany(re =>
        re.CreateSourceQuery().OfType<EntityObject>()).Distinct()).ToArray())
            {
                _ctx.DeleteObject(relatedEntity);
            }//end foreach
        }
        /// <summary>
        /// Delete all related entries
        /// </summary>
        /// <param name="entity"></param>        
        public void DeleteRelatedEntries(E entity, ObservableCollection<string>
                            keyListOfIgnoreEntites)
        {
            foreach (var relatedEntity in (((IEntityWithRelationships)entity).
            RelationshipManager.GetAllRelatedEnds().SelectMany(re =>
            re.CreateSourceQuery().OfType<EntityObject>()).Distinct()).ToArray())
            {
                PropertyInfo propInfo = relatedEntity.GetType().GetProperty
                            (this._KeyProperty);
                if (null != propInfo)
                {
                    string value = (string)propInfo.GetValue(relatedEntity, null);
                    if (!string.IsNullOrEmpty(value) &&
                        keyListOfIgnoreEntites.Contains(value))
                    {
                        continue;
                    }//end if 
                }//end if
                _ctx.DeleteObject(relatedEntity);
            }//end foreach
        }

        #endregion
        #region Etc
        public void AddToT<T>(ref T o)
        {
            string entitySetName = _ctx.MetadataWorkspace
                                    .GetEntityContainer(_ctx.DefaultContainerName, DataSpace.CSpace)
                                    .BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(T).Name).First().Name;

            string name = String.Format("{0}.{1}", _ctx.DefaultContainerName, entitySetName);

            _ctx.AddObject(name, o);

        }
        public void AttachToT<T>(ref T o)
            where T : class
        {
            ObjectSet<T> objSet = _ctx.CreateObjectSet<T>();
            
            objSet.Attach(o);
       

        }

        public Boolean ExistsInT<T>(ref T o, int instance)
            where T : class
        {
            Boolean bExists = false;

            ObjectSet<T> objSet = _ctx.CreateObjectSet<T>();



            var keyPropertyName = objSet.EntitySet.ElementType.KeyMembers[0].ToString();

            T entity = objSet.Where("it." + keyPropertyName + "=" + instance).First();

            if (entity != null)
                bExists = true;


            return bExists;
        }

        public int CountOfInRepository<T>()
        {
            int i = 0;
        string entitySetName = _ctx.MetadataWorkspace
                                    .GetEntityContainer(_ctx.DefaultContainerName, DataSpace.CSpace)
                                    .BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(T).Name).First().Name;

            string name = String.Format("{0}.{1}", _ctx.DefaultContainerName, entitySetName);
            EntityContainer ec = _ctx.MetadataWorkspace
                                    .GetEntityContainer(_ctx.DefaultContainerName, DataSpace.CSpace);

            EntitySet es = ec.GetEntitySetByName(entitySetName, false);
      
            return 0;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (null != _ctx)
            {
                _ctx.Dispose();
            }
        }

        #endregion

    }
}