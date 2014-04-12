using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlueBoxSharp.Data.Expressions
{
    public class InsertSelectExpression : QueryExpression
    {
        internal InsertSelectExpression(QueryExpression expression)
            : base(expression.Context, expression.Type)
        {
        }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.InsertSelect; }
        }
    }
}
