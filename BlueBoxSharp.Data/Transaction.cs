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
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using System.Threading.Tasks;
using BlueBoxSharp.Collections;

namespace BlueBoxSharp.Data
{
    public abstract partial class DataContext
    {
        // TODO: Add arbitrary SQL command
        public class Transaction : IDisposable
        {
            private DataContext _context;
            private DbTransaction _dbTransaction;

            internal Transaction(DataContext context)
            {
                this._context = context;
                this._dbTransaction = context.GetConnection().BeginTransaction();
            }

            public void Insert<TEntity>(TEntity entity) where TEntity : Entity<TEntity>, new()
            {
                try
                {
                    this._context.Insert(entity, this._dbTransaction.Connection, this._dbTransaction);
                }
                catch (Exception e)
                {
                    this._dbTransaction.Rollback();
                    entity.EntityIdentity = null;

                    throw e;
                }
            }

            public void Update<TEntity>(TEntity entity) where TEntity : Entity<TEntity>, new()
            {
                try
                {
                    this._context.Update(entity, this._dbTransaction.Connection, this._dbTransaction);
                }
                catch (Exception e)
                {
                    this._dbTransaction.Rollback();
                    throw e;
                }
            }

            public void Delete<TEntity>(TEntity entity) where TEntity : Entity<TEntity>, new()
            {
                try
                {
                    this._context.Delete(entity, this._dbTransaction.Connection, this._dbTransaction);
                }
                catch (Exception e)
                {
                    this._dbTransaction.Rollback();
                    throw e;
                }
            }


            public TResult ExecuteScalar<TResult>(string query, params DbParameter[] parameters)
            {
                try
                {
                    if (!TypeHelper.IsBaseType(typeof(TResult)))
                        return default(TResult);

                    return this._context.ExecuteScalar<TResult>(this._context.CreateCommand(this._dbTransaction.Connection, this._dbTransaction, query));
                }
                catch (Exception e)
                {
                    this._dbTransaction.Rollback();
                    throw e;
                }
            }

            public void ExecuteNonQuery(string query, params DbParameter[] parameters)
            {
                try
                {
                    DbCommand command = this._context.CreateCommand(this._dbTransaction.Connection, this._dbTransaction, query);
                    command.Parameters.AddRange(parameters);
                    this._context.ExecuteNonQuery(command);
                }
                catch (Exception e)
                {
                    this._dbTransaction.Rollback();
                    throw e;
                }
            }

            public TResult ExecuteProcedureScalar<TResult>(string procedure, params object[] parameters)
            {
                try
                {
                    if (!TypeHelper.IsBaseType(typeof(TResult)))
                        return default(TResult);

                    return this._context.ExecuteScalar<TResult>(
                            this._context.CreateCommand(this._dbTransaction.Connection, this._dbTransaction,
                                this._context.CreateProcedureCall(procedure, parameters))
                        );
                }
                catch (Exception e)
                {
                    this._dbTransaction.Rollback();
                    throw e;
                }
            }

            public void ExecuteProcedureNonQuery(string procedure, params object[] parameters)
            {
                try
                {
                    DbCommand command = this._context.CreateCommand(this._dbTransaction.Connection, this._dbTransaction, this._context.CreateProcedureCall(procedure, parameters));
                    this._context.ExecuteNonQuery(command);
                }
                catch (Exception e)
                {
                    this._dbTransaction.Rollback();
                    throw e;
                }
            }
            
#if !NET40

            async public Task<TResult> ExecuteScalarAsync<TResult>(string query, params DbParameter[] parameters)
            {
                if (!TypeHelper.IsBaseType(typeof(TResult)))
                    return default(TResult);

                using (DbCommand command = this._context.CreateCommand(this._dbTransaction.Connection, this._dbTransaction, query))
                {
                    command.Parameters.AddRange(parameters);

                    object value = await command.ExecuteScalarAsync() ?? default(TResult);

                    if (value is DBNull)
                        return default(TResult);

                    return (TResult)value;
                }
            }

            async public Task ExecuteNonQueryAsync(string query, params DbParameter[] parameters)
            {
                using (DbCommand command = this._context.CreateCommand(this._dbTransaction.Connection, this._dbTransaction, query))
                {
                    command.Parameters.AddRange(parameters);
                    await command.ExecuteNonQueryAsync();
                }
            }

            async public Task<TResult> ExecuteProcedureScalarAsync<TResult>(string procedure, params object[] parameters)
            {
                if (!TypeHelper.IsBaseType(typeof(TResult)))
                    return default(TResult);

                using (DbCommand command = this._context.CreateCommand(this._dbTransaction.Connection, this._dbTransaction, this._context.CreateProcedureCall(procedure, parameters)))
                {
                    object value = await command.ExecuteScalarAsync() ?? default(TResult);

                    if (value is DBNull)
                        return default(TResult);

                    return (TResult)value;
                }
            }

            async public Task ExecuteProcedureNonQueryAsync(string procedure, params object[] parameters)
            {
                using (DbCommand command = this._context.CreateCommand(this._dbTransaction.Connection, this._dbTransaction, this._context.CreateProcedureCall(procedure, parameters)))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }

#endif

            public void Dispose()
            {
                try
                {
                    this._dbTransaction.Commit();
                }
                finally
                {
                    this._dbTransaction.Connection.Close();
                }
            }
        }
    }
}
