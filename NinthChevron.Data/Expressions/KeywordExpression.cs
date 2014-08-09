using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NinthChevron.Data.Expressions
{
    public class KeywordExpression : Expression
    {
        public KeywordExpression(object value)
        {
            this.Value = value;
        }

        public object Value { get; private set; }
        public Expression Expression { get; private set; }
        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.Keyword; }
        }

        public override Type Type
        {
            get { return this.Value.GetType(); }
        }
    }
}
