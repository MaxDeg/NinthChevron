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
using System.Reflection;
using System.Text;

namespace BlueBoxSharp.Utilities
{
    public static class ObjectMerger
    {
        public static TType Merge<TType>(TType source, TType data, string[] changedProperties)
        {
            Type tp = source.GetType();

            foreach (string property in changedProperties)
                SetPropertyValue(source, data, property);

            return source;
        }

        private static void SetPropertyValue(object dest, object source, string propertyName)
        {
            string[] properties = propertyName.Split('.');
            PropertyInfo prop = null;
            object currentDest = dest;
            object currentSource = source;

            foreach (string property in properties)
            {
                currentDest = prop != null ? prop.GetValue(currentDest, null) : currentDest;
                prop = currentDest.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                prop = currentSource.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                currentSource = prop.GetValue(currentSource, null);
            }

            prop.SetValue(currentDest, currentSource, null);
        }
    }
}
