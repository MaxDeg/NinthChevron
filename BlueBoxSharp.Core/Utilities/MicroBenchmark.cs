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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueBoxSharp.Utilities
{
    public class MicroBenchmark : IDisposable
    {
        private Stopwatch _stopWatch;
        private string _label;
        private SimpleLogger _logger;

        public MicroBenchmark(string label)
            : this(label, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MicroBenchmark.log"))
        { }

        public MicroBenchmark(string label, string path)
        {
            this._label = label;
            this._logger = new SimpleLogger(path);
            this._stopWatch = new Stopwatch();
            this._stopWatch.Start();
        }

        public void Dispose()
        {
            this._stopWatch.Stop();
            string log = "[" + DateTime.Now.ToString("hh:mm:ss") + "][MicroBenchmark][" + this._stopWatch.ElapsedMilliseconds + "ms] " + this._label;

            Debug.WriteLine(log);
            this._logger.Log(log);
        }
    }
}
