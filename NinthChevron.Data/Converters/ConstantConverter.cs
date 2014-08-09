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
using System.Linq.Expressions;
using System.Text;
using NinthChevron.Data.Expressions;

namespace NinthChevron.Data.Converters
{
    internal class ConstantConverter : BaseConverter<ConstantExpression>
    {
        internal ConstantConverter() : base(ExpressionType.Constant) { }

        public override Expression Convert(ExpressionConverter converter, ConstantExpression expression)
        {
            if (typeof(IInternalQuery).IsAssignableFrom(expression.Type))
            {
                IInternalQuery set = (IInternalQuery)expression.Value;
                return converter.CreateQuery(set.Context, expression.Type.GetGenericArguments()[0]);
            }

            object[] attributes = expression.Type.GetCustomAttributes(typeof(SqlKeywordAttribute), true);
            if (attributes != null && attributes.Length > 0)
                return new KeywordExpression(expression.Value);

            return expression;
        }
    }
}
