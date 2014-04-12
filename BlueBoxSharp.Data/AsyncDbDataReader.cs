using BlueBoxSharp.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Data
{
#if !NET40

    internal class AsyncDbDataReader<TClass> : IAsyncEnumerable<TClass>
    {
        private DbConnection _connection;
        private DbCommand _command;
        private Func<IDataRecord, TClass> _recordReader;
        private int _skipCount;

        public AsyncDbDataReader(DbConnection connection, DbCommand command, int skipCount, Func<IDataRecord, TClass> recordReader)
        {
            this._connection = connection;
            this._command = command;
            this._recordReader = recordReader;
            this._skipCount = skipCount;
        }

        public IAsyncEnumerator<TClass> GetAsyncEnumerator()
        {
            return new Enumerator(this._connection, this._command, this._skipCount, this._recordReader);
        }

        public class Enumerator : IAsyncEnumerator<TClass>
        {
            private DbConnection _connection;
            private DbCommand _command;
            private DbDataReader _reader;
            private Func<IDataRecord, TClass> _recordReader;
            private bool _isExecuted;
            private TClass _current;
            private int _skipCount;

            public Enumerator(DbConnection connection, DbCommand command, int skipCount, Func<IDataRecord, TClass> recordReader)
            {
                this._connection = connection;
                this._reader = null;
                this._current = default(TClass);
                this._isExecuted = false;

                this._command = command;
                this._recordReader = recordReader;
                this._skipCount = skipCount;
            }

            public TClass Current { get { return this._current; } }

            async public Task<bool> MoveNextAsync()
            {
                if (this._isExecuted && this._reader == null)
                    return false;

                if (this._reader == null)
                {
                    this._isExecuted = true;
                    await this._connection.OpenAsync();
                    this._reader = await this._command.ExecuteReaderAsync();
                }

                bool result = false;

                do
                {
                    result = await this._reader.ReadAsync();
                    this._skipCount--;
                }
                while (result && this._skipCount > 0);

                if (result)
                    this._current = this._recordReader(this._reader);

                return result;
            }

            public Task ResetAsync()
            {
                this._isExecuted = false;
                this._reader = null;

                if (this._connection.State == ConnectionState.Open)
                    this._connection.Close();

                return Task.FromResult(true);
            }

            public void Dispose()
            {
                if (this._reader != null)
                    this._reader.Dispose();

                if (this._connection.State == ConnectionState.Open)
                    this._connection.Close();
            }
        }
    }

#endif
}
