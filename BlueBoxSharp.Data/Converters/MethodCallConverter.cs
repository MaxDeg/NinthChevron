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
using BlueBoxSharp.Data.Converters.IQueryableConverters;
using BlueBoxSharp.Data.Expressions;

namespace BlueBoxSharp.Data.Converters
{
    internal class MethodCallConverter : BaseConverter<MethodCallExpression>
    {
        private List<Type> _queryableTypes = new List<Type> { typeof(Queryable), typeof(EntityQueryable), typeof(IQueryableExtensions) };

        internal MethodCallConverter()
            : base(ExpressionType.Call)
        {
        }

        public override Expression Convert(ExpressionConverter converter, MethodCallExpression expression)
        {
            var customAttr = expression.Method.GetCustomAttributes(typeof(SqlFunctionAttribute), false);

            if (customAttr != null && customAttr.Length > 0)
            {
                return expression.Update(expression.Object, expression.Arguments.Select(a => converter.Convert(a)));
            }
            else if (expression.Object == null && _queryableTypes.Contains(expression.Method.DeclaringType))
            {
                IQueryableMethodConverter methodConverter;
                if (_converters.TryGetValue(expression.Method.Name, out methodConverter))
                    return methodConverter.Call(converter, expression);

                throw new Exception("This method is not implemented in the Provider");
            }
            else if (expression.Arguments.Count > 0 && expression.Arguments[0].Type.IsGenericType 
                            && expression.Arguments[0].Type.GetGenericTypeDefinition() == typeof(IGrouping<,>))
            {
                switch (expression.Method.Name)
                {
                    case "Count":
                        return new AggregateExpression(typeof(int), ExtendedExpressionType.Count, null);
                    case "Max":
                        return new AggregateExpression(typeof(int), ExtendedExpressionType.Max, null);
                    case "Min":
                        return new AggregateExpression(typeof(int), ExtendedExpressionType.Min, null);
                    case "Sum":
                        return new AggregateExpression(typeof(int), ExtendedExpressionType.Sum, null);
                    case "Average":
                        return new AggregateExpression(typeof(int), ExtendedExpressionType.Average, null);
                }
            }
            else if (expression.Object != null)
            {
                Expression instance = converter.Convert(expression.Object);
                List<Expression> arguments = expression.Arguments.Select(a => converter.Convert(a)).ToList();

                if (instance is ConstantExpression && !arguments.Any(a => a.NodeType != ExpressionType.Constant))
                {
                    Expression objectMember = Expression.Convert(expression, typeof(object));
                    Expression<Func<object>> getterLambda = Expression.Lambda<Func<object>>(objectMember);
                    object getter = getterLambda.Compile()();

                    if (getter != null && typeof(IInternalQuery).IsAssignableFrom(getter.GetType()))
                    {
                        IInternalQuery set = (IInternalQuery)getter;
                        return converter.CreateQuery(set.Context, getter.GetType().GetGenericArguments()[0]);
                    }
                    else
                        return Expression.Constant(expression.Method.Invoke(
                                ((ConstantExpression)instance).Value,
                                arguments.Select(a => ((ConstantExpression)a).Value).ToArray()
                            ), expression.Method.ReturnType);
                }

                return Expression.Call(instance, expression.Method, arguments);
            }
            else
            {
                List<Expression> arguments = expression.Arguments.Select(a => converter.Convert(a)).ToList();
                return expression.Update(expression.Object, arguments);
            }

            return expression;
        }

        #region IQueryable Methods

        private static Dictionary<string, IQueryableMethodConverter> _converters = InitConverters();

        private static Dictionary<string, IQueryableMethodConverter> InitConverters()
        {
            Dictionary<string, IQueryableMethodConverter> converters = new Dictionary<string, IQueryableMethodConverter>();

            foreach (IQueryableMethodConverter converter in Converters())
                foreach (string method in converter.Methods)
                    converters.Add(method, converter);

            return converters;
        }

        private static IEnumerable<IQueryableMethodConverter> Converters()
        {
            yield return new FirstMethodConverter();
            yield return new SelectMethodConverter();
            yield return new InsertMethodConverter();
            yield return new UpdateMethodConverter();
            yield return new DeleteMethodConverter();
            yield return new WhereMethodConverter();
            yield return new GroupByMethodConverter();
            yield return new CountMethodConverter();
            yield return new AggregateMethodConverter();
            yield return new AnyMethodConverter();
            yield return new TakeMethodConverter();
            yield return new SkipMethodConverter();
            yield return new OrderByMethodConverter();
            yield return new UnionMethodConverter();
            yield return new DistinctMethodConverter();
            yield return new JoinMethodConverter();
            yield return new WithIndexMethodConverter();
            yield return new ContainsMethodConverter();
        }

        #endregion
    }
}
