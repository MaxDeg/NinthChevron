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
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Timers;

namespace BlueBoxSharp.Utilities
{
	public class SimpleLogger
	{
		private class LogEntry
		{
			public DateTime Date { get; private set; }
			public object Data { get; private set; }

			public LogEntry(object data)
			{
				this.Date = DateTime.Now;
				this.Data = data;
			}
		}

		private string _logPath;
		private ConcurrentQueue<LogEntry> _logQueue;
		private Timer _writeTimer;
		private object _syncObject = new object();

        public SimpleLogger(string path)
		{
			this._logPath = path;
			this._logQueue = new ConcurrentQueue<LogEntry>();

			this._writeTimer = new Timer
			{
				AutoReset = false,
				Enabled = false,
				Interval = 2000
			};
			this._writeTimer.Elapsed += new ElapsedEventHandler(this.WriteEntries);
		}

		public void Log(object entry)
		{
			this._logQueue.Enqueue(new LogEntry(entry));
			if (!this._writeTimer.Enabled) this._writeTimer.Start();
		}

		public static string LogDictionary(IDictionary dictionary)
		{
			string result = "\r\n";

			if (dictionary != null)
			{
				foreach (var key in dictionary.Keys)
					if (key != null && dictionary[key] != null)
						result += key + ": " + dictionary[key] + "\r\n";
			}

			return result;
		}

		public static string LogNameValueCollection(NameValueCollection dictionary)
		{
			string result = "\r\n";

			if (dictionary != null)
			{
				foreach (string key in dictionary.Keys)
					if (key != null && dictionary[key] != null)
						result += key + ": " + dictionary[key] + "\r\n";
			}

			return result;
		}

		private void WriteEntries(object sender, ElapsedEventArgs e)
		{
			try
			{
				string file = Path.GetFileNameWithoutExtension(this._logPath) + "-" + DateTime.Now.ToString("yyyyMMdd") + ".log";
				string path = Path.Combine(Path.GetDirectoryName(this._logPath), file);
                if (!Directory.Exists(Path.GetDirectoryName(this._logPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(this._logPath));

				using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write)))
				{
					LogEntry entry;
					while (this._logQueue.TryDequeue(out entry))
					{
						writer.WriteLine(entry.Date.ToString("yyyy-MM-dd HH:mm:ss"));

						Type dataType = entry.Data.GetType();
                        if (dataType == typeof(string))
                            writer.WriteLine("\t" + entry.Data);
                        else
						    foreach (PropertyInfo property in dataType.GetProperties())
							    writer.WriteLine("\t" + property.Name + ": " + property.GetValue(entry.Data, null));

						writer.WriteLine("----------------------------------------------------------------");
					}
				}
			}
			catch (Exception ex)
			{
				string file = Path.GetFileNameWithoutExtension(this._logPath) + ".log";
				string path = Path.Combine(Path.GetDirectoryName(this._logPath), file);

				File.AppendAllText(path, ex.Message + "\r\n" + ex.StackTrace + "\r\n");
			}
		}
	}
}
