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
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using NinthChevron.Utilities;
using System.IO;
using NinthChevron.ILInjector.Injectors;

namespace NinthChevron.ILInjector.Build
{
    public class InjectCode : ITask
    {
        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }

        [Required]
        public string Assembly { get; set; }

        public bool Execute()
        {
            try
            {
                NotifyPropertyChangesInjector injector = new NotifyPropertyChangesInjector(Assembly);
                injector.Run();
                
                return true;
            }
            catch (Exception e)
            {
                SimpleLogger logger = new SimpleLogger(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "code.injection.log"));
                logger.Log(e);

                return false;
            }
        }

    }
}
