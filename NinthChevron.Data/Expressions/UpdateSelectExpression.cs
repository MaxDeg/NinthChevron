using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NinthChevron.Data.Expressions
{
    public class UpdateSelectExpression : QueryExpression
    {
        internal UpdateSelectExpression(QueryExpression expression)
            : base(expression.Context, expression.Type)
        {
        }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.UpdateSelect; }
        }
    }
}
