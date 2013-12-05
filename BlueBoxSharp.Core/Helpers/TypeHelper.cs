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

namespace BlueBoxSharp.Helpers
{
    public static class TypeHelper
    {
        private static List<Type> BaseTypes = new List<Type>()
        {
            typeof(int),        typeof(int?),
            typeof(short),      typeof(short?),
            typeof(double),     typeof(double?),
            typeof(float),      typeof(float?),
            typeof(long),       typeof(long?),
            typeof(char),       typeof(char?),
            typeof(string), 
            typeof(DateTime),   typeof(DateTime?),
            typeof(bool),       typeof(bool?),
            typeof(decimal),    typeof(decimal?),
            typeof(object)
        };

        public static bool IsBaseType(Type type)
        {
            return BaseTypes.Contains(type) || type.IsEnum;
        }

        public static Type NonNullableType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return type.GetGenericArguments()[0];

            return type;
        }

        public static object GetDefault(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        public static bool IsNullable(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static T GetDefault<T>()
        {
            var t = typeof(T);
            return (T)GetDefault(t);
        }

        public static bool IsDefault<T>(T other)
        {
            T defaultValue = GetDefault<T>();
            if (other == null) return defaultValue == null;
            return other.Equals(defaultValue);
        }

        public static bool IsDefault(object other)
        {
            if (other == null)
                return true;

            object defaultValue = GetDefault(other.GetType());

            return other.Equals(defaultValue);
        }

        public static bool IsNumeric(Type t)
        {
            Type realType = NonNullableType(t);

            return realType == typeof(int) || realType == typeof(short) || realType == typeof(double)
                    || realType == typeof(float) || realType == typeof(long) || realType == typeof(decimal);
        }
    }
}
