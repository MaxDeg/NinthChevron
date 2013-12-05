/**
 *   Copyright 2013
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BlueBoxSharp.Data.Converters;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Data.Expressions;
using BlueBoxSharp.Data.Metadata;
using BlueBoxSharp.Data.Translators;
using BlueBoxSharp.Helpers;

namespace BlueBoxSharp.Data
{
    public abstract partial class DataContext
    {
        private readonly Dictionary<Type, IInternalQuery> _sets = new Dictionary<Type, IInternalQuery>();

        protected ITranslator Translator { get; set; }
        protected string ConnectionString { get; private set; }

        protected virtual void OnChangeSaved(DbConnection connection, DbTransaction transaction, IEntityTracker tracker) { }
        protected virtual void OnQueryExecuted(string query) { }

        protected abstract DbCommand CreateCommand(DbConnection connection, DbTransaction transaction, string query);
        protected abstract DbConnection OpenConnection();

        public DataContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        #region Strong Entity Methods

        public IOrderedQueryable<TEntity> Query<TEntity>() where TEntity : Entity<TEntity>, new()
        {
            IInternalQuery set;
            Type type = typeof(TEntity);
            if (!_sets.TryGetValue(type, out set))
            {
                set = new Query<TEntity>(new QueryProvider(), this);
                this._sets.Add(type, set);
            }

            return (IOrderedQueryable<TEntity>)set;
        }

        /// <summary>
        /// Not Yet Implemented. Throw a NotImplementedException
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public void CreateTable<TEntity>() where TEntity : Entity<TEntity>, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Yet Implemented. Throw a NotImplementedException
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public void DropTable<TEntity>() where TEntity : Entity<TEntity>, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Yet Implemented. Throw a NotImplementedException
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public void TruncateTable<TEntity>() where TEntity : Entity<TEntity>, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute directly the insert command on the DB
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(IEntity entity)
        {
            using (DbConnection connection = OpenConnection())
                this.Insert(entity, connection, null);
        }

        /// <summary>
        /// Execute directly the update command on the DB
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IEntity entity)
        {
            using (DbConnection connection = OpenConnection())
                this.Update(entity, connection, null);
        }

        /// <summary>
        /// Execute directly the delete command on the DB
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(IEntity entity)
        {
            using (DbConnection connection = OpenConnection())
                this.Delete(entity, connection, null);
        }

        #endregion

        // Should return a IDisposable
        public Transaction CreateTransaction()
        {
            return new Transaction(this);
        }

        #region Internal Execution

        internal void Insert(IEntity entity, DbConnection connection, DbTransaction transaction)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            bool track = false;
            if (entity.GetType().GetCustomAttributes(typeof(DoNotTrackAttribute), true).Length == 0)
                track = true;

            // Add to the list of object to be inserted/updated
            // Care about property changes
            InsertExpression command = new InsertExpression(this, entity as IInternalEntity);

            EntityInsertTracker tracker = null;
            if (track) tracker = new EntityInsertTracker(entity);

            object id = this.ExecuteScalar<object>(this.CreateCommand(connection, transaction, command));
            entity.EntityIdentity = id;

            if (tracker != null)
                this.OnChangeSaved(connection, transaction, tracker);
        }

        internal void Update(IEntity entity, DbConnection connection, DbTransaction transaction)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Type entityType = entity.GetType();
            bool track = false;
            if (entityType.GetCustomAttributes(typeof(DoNotTrackAttribute), true).Length == 0)
                track = true;

            // Add to the list of object to be inserted/updated
            // Care about property changes
            TableMetadata meta = MappingProvider.GetMetadata(entityType);
            IEnumerable<Tuple<string, object>> conditions;

            UpdateExpression command = new UpdateExpression(this, entity as IInternalEntity);

            EntityChangeTracker tracker = null;
            if (track) tracker = ((IInternalEntity)entity).ChangeTracker;

            // Add condition to query
            Tuple<string, object> identity = meta.GetIdentityValue(entity);
            if (identity != null)
                conditions = new List<Tuple<string, object>> { identity };
            else
                conditions = meta.GetPrimaryKeyValues(entity);

            this.AddCondition(command, entityType, conditions);

            this.ExecuteNonQuery(this.CreateCommand(connection, transaction, command));

            if (tracker != null)
                this.OnChangeSaved(connection, transaction, tracker);
        }

        internal void Delete(IEntity entity, DbConnection connection, DbTransaction transaction)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Type entityType = entity.GetType();
            TableMetadata meta = MappingProvider.GetMetadata(entityType);

            // Add to the list of object to be deleted
            DeleteExpression command = new DeleteExpression(this, entity);
            IEnumerable<Tuple<string, object>> conditions;

            // Add condition to query
            Tuple<string, object> identity = meta.GetIdentityValue(entity);
            if (identity != null)
                conditions = new List<Tuple<string, object>> { identity };
            else
                conditions = meta.GetPrimaryKeyValues(entity);

            this.AddCondition(command, entityType, conditions);

            EntityDeleteTracker tracker = null;
            if (entityType.GetCustomAttributes(typeof(DoNotTrackAttribute), true).Length == 0)
                tracker = new EntityDeleteTracker(entity);

            this.ExecuteNonQuery(this.CreateCommand(connection, transaction, command));
            entity.EntityIdentity = null;

            if (tracker != null)
                this.OnChangeSaved(connection, transaction, tracker);
        }

        internal TResult ExecuteCommand<TResult>(CommandExpression expression, IEntityTracker tracker)
        {
            TResult result = default(TResult);

            using (DbConnection connection = OpenConnection())
            using (DbCommand command = CreateCommand(connection, null, expression))
            {
                if (expression is InsertExpression)
                    result = ExecuteScalar<TResult>(command);
                else
                    ExecuteNonQuery(command);

                if (tracker != null)
                    this.OnChangeSaved(connection, null, tracker);

                return result;
            }
        }

        internal DbCommand CreateCommand(DbConnection connection, DbTransaction transaction, CommandExpression expression)
        {
            string query = this.Translator.Translate(expression);

            if (expression is InsertExpression)
                return CreateCommand(connection, transaction, query);
            else
                return CreateCommand(connection, transaction, query);
        }

        internal IEnumerable<TResult> Execute<TResult>(QueryExpression expression)
        {
            List<TResult> list = new List<TResult>();

            string query = this.Translator.Translate(expression);
            if (string.IsNullOrEmpty(query)) return list;

            this.OnQueryExecuted(query);

            // Create ResultReader
            QueryResultReader queryReader = new QueryResultReader(expression.Projection);
            int skipCount = expression.SkipCount;

            using (DbConnection connection = OpenConnection())
            {
                using (DbCommand command = CreateCommand(connection, null, query))
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (skipCount-- > 0) continue;

                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);

                        // Read result
                        object result = queryReader.Read(values);

                        if (result != null) list.Add((TResult)Convert.ChangeType(result, typeof(TResult)));
                        else list.Add(default(TResult));
                    }

                    return list;
                }
            }
        }

        #endregion

        #region SQL Query/Command

        public TResult ExecuteScalar<TResult>(string query, params DbParameter[] parameters)
        {
            if (!TypeHelper.IsBaseType(typeof(TResult)))
                return default(TResult);

            using (DbConnection connection = OpenConnection())
                return ExecuteScalar<TResult>(CreateCommand(connection, null, query));
        }

        public IEnumerable<DataRecord> Execute(string query, params DbParameter[] parameters)
        {
            using (DbConnection connection = OpenConnection())
            {
                DbCommand command = CreateCommand(connection, null, query);
                command.Parameters.AddRange(parameters);

                return Execute(command);
            }
        }

        public void ExecuteNonQuery(string query, params DbParameter[] parameters)
        {
            using (DbConnection connection = OpenConnection())
            {
                DbCommand command = CreateCommand(connection, null, query);
                command.Parameters.AddRange(parameters);
                ExecuteNonQuery(command);
            }
        }


        public TResult ExecuteProcedureScalar<TResult>(string procedure, params object[] parameters)
        {
            if (!TypeHelper.IsBaseType(typeof(TResult)))
                return default(TResult);

            using (DbConnection connection = OpenConnection())
                return ExecuteScalar<TResult>(CreateCommand(connection, null, CreateProcedureCall(procedure, parameters)));
        }

        public IEnumerable<IEnumerable<DataRecord>> ExecuteMultiResultSetProcedure(string procedure, params object[] parameters)
        {
            using (DbConnection connection = OpenConnection())
            using (DbCommand command = CreateCommand(connection, null, CreateProcedureCall(procedure, parameters)))
            using (DbDataReader reader = command.ExecuteReader())
            {
                do
                    yield return DataRecord.CreateEnumerable(reader);
                while (reader.NextResult());
            }
        }

        public IEnumerable<DataRecord> ExecuteProcedure(string procedure, params object[] parameters)
        {
            using (DbConnection connection = OpenConnection())
                return Execute(CreateCommand(connection, null, CreateProcedureCall(procedure, parameters)));
        }

        public void ExecuteProcedureNonQuery(string procedure, params object[] parameters)
        {
            using (DbConnection connection = OpenConnection())
            {
                DbCommand command = CreateCommand(connection, null, CreateProcedureCall(procedure, parameters));
                ExecuteNonQuery(command);
            }
        }

        #endregion


        private TResult ExecuteScalar<TResult>(DbCommand command)
        {
            if (!TypeHelper.IsBaseType(typeof(TResult)))
                return default(TResult);

            this.OnQueryExecuted(command.CommandText);

            using (command)
            {
                object value = command.ExecuteScalar() ?? default(TResult);

                if (value is DBNull)
                    return default(TResult);

                return (TResult)value;
            }
        }

        private IEnumerable<DataRecord> Execute(DbCommand command)
        {
            this.OnQueryExecuted(command.CommandText);

            using (command)
            {
                using (DbDataReader reader = command.ExecuteReader())
                {
                    foreach (IDataRecord r in reader)
                        yield return new DataRecord(r);
                }
            }
        }

        private void ExecuteNonQuery(DbCommand command)
        {
            this.OnQueryExecuted(command.CommandText);
            using (command) command.ExecuteNonQuery();
        }
        
        private string CreateProcedureCall(string procedure, object[] parameters)
        {
            return "EXEC " + procedure + " " + string.Join(", ", parameters.Select(p => Translator.SqlEncode(p)));
        }

        private void AddCondition(CommandExpression command, Type entityType, IEnumerable<Tuple<string, object>> conditions)
        {
            if (conditions == null) throw new ArgumentNullException("conditions");

            foreach (Tuple<string, object> value in conditions)
            {
                ExpressionConverter converter = new ExpressionConverter(null);
                ParameterExpression param = Expression.Parameter(entityType);
                Expression conditionClause = Expression.Equal(
                        Expression.MakeMemberAccess(param, entityType.GetProperty(value.Item1)),
                        Expression.Constant(value.Item2)
                        );

                conditionClause = converter.Convert(conditionClause, new Binding(param, command.From));

                if (command.Where == null)
                    command.Where = conditionClause;
                else
                    command.Where = Expression.AndAlso(command.Where, conditionClause);
            }
        }
    }
}