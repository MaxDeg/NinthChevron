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
using System.Reflection;
using System.Text;
using BlueBoxSharp.Data.Expressions;

namespace BlueBoxSharp.Data.Converters
{
    internal class ExpressionConverter
    {
        private Expression _expression;
        private QueryExpression _rootQuery;
        
        internal ExpressionConverter(Expression expression)
        {
            this.Context = new BindingContext();
            this._expression = expression;
        }

        internal ExpressionConverter(Expression expression, QueryExpression query)
        {
            this.Context = new BindingContext();
            this._expression = expression;
            this._rootQuery = query;
        }

        internal BindingContext Context { get; private set; }

        internal Expression Convert()
        {
            return Convert(this._expression);
        }

        internal Expression Convert(Expression expression)
        {
            IConverter converter;
            if (_converters.TryGetValue(expression.NodeType, out converter))
                return converter.Convert(this, expression);

            throw new InvalidOperationException("Converter Not Found");
        }

        internal Expression Convert(Expression expression, params Binding[] bindings)
        {
            try
            {
                this.Context.ScopeBegin(bindings);

                IConverter converter;
                if (_converters.TryGetValue(expression.NodeType, out converter))
                    return converter.Convert(this, expression);

                throw new InvalidOperationException("Converter Not Found");
            }
            finally
            {
                this.Context.ScopeEnd();
            }
        }

        internal LambdaExpression GetLambdaExpression(Expression node)
        {
            while (node.NodeType == ExpressionType.Quote)
            {
                node = ((UnaryExpression)node).Operand;
            }

            return node as LambdaExpression;
        }
        
        internal QueryExpression CreateQuery(DataContext context, Type type)
        {
            if (this._rootQuery == null)
            {
                this._rootQuery = new QueryExpression(context, type);
                return this._rootQuery;
            }
            else
                return this._rootQuery.SubQuery(type);
        }

        #region Converters

        private static Dictionary<ExpressionType, IConverter> _converters = InitConverters();

        private static Dictionary<ExpressionType, IConverter> InitConverters()
        {
            Dictionary<ExpressionType, IConverter> converters = new Dictionary<ExpressionType, IConverter>();

            foreach (IConverter converter in Converters())
                foreach (ExpressionType type in converter.ExpressionTypes)
                    converters.Add(type, converter);

            return converters;
        }

        private static IEnumerable<IConverter> Converters()
        {
            yield return new MethodCallConverter();
            yield return new ParameterConverter();
            yield return new MemberAccessConverter();
            yield return new ConstantConverter();
            yield return new MemberInitConverter();
            yield return new NewConverter();
            yield return new UnaryConverter();
            yield return new BinaryConverter();
            yield return new ConditionalConverter();
            yield return new ListInitConverter();
            yield return new NewArrayInitConverter();
        }
        
        #endregion
    }
}
