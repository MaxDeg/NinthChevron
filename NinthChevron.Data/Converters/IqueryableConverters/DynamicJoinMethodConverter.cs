using NinthChevron.Data.Expressions;
using NinthChevron.Data.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Data.Converters.IQueryableConverters
{
    internal class DynamicJoinMethodConverter : BaseIQueryableMethodConverter
    {
        public DynamicJoinMethodConverter() : base("DynamicJoin") { }

        public override Expression Call(ExpressionConverter converter, MethodCallExpression expression)
        {
            QueryExpression context = (QueryExpression)converter.Convert(expression.Arguments[0]);
            LambdaExpression lambda;

            ConstantExpression joinType = (ConstantExpression)converter.Convert(expression.Arguments[1]);

            lambda = converter.GetLambdaExpression(expression.Arguments[2]);
            Expression propSelector = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], context.Projection));

            QueryExpression joinEntity = (QueryExpression)converter.Convert(expression.Arguments[3]);

            lambda = converter.GetLambdaExpression(expression.Arguments[4]);
            Expression clause = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], propSelector), new Binding(lambda.Parameters[1], joinEntity.Projection));

            JoinExpression joinExpr = new JoinExpression(Guid.NewGuid().ToString(), (EntityExpression)joinEntity.From, (JoinType)joinType.Value, clause);

            // Find an other way !!!!
            if (propSelector.NodeType == (ExpressionType)ExtendedExpressionType.Query)
                ((EntityExpression)context.From).Join(converter, joinExpr);
            else if (propSelector.NodeType == (ExpressionType)ExtendedExpressionType.Entity)
                ((EntityExpression)propSelector).Join(converter, joinExpr);
            else if (propSelector.NodeType == (ExpressionType)ExtendedExpressionType.EntityReference)
                ((EntityRefExpression)propSelector).Entity.Join(converter, joinExpr);
            else if (propSelector.NodeType == (ExpressionType)ExtendedExpressionType.Join)
                ((JoinExpression)propSelector).Entity.Join(converter, joinExpr);
            else if (propSelector.NodeType == (ExpressionType)ExtendedExpressionType.Projection)
            {
                EntityProjectionExpression proj = (EntityProjectionExpression)propSelector;
                proj.Entity.Join(converter, joinExpr);
            }

            lambda = converter.GetLambdaExpression(expression.Arguments[5]);
            Expression projection = converter.Convert(lambda.Body,
                                                new Binding(lambda.Parameters[0], context.Projection),
                                                new Binding(lambda.Parameters[1], joinEntity.Projection)
                                                );

            if (projection.NodeType == (ExpressionType)ExtendedExpressionType.Projection)
                context.Project(projection as ProjectionExpression);
            else
                context.Project(new ProjectionExpression(projection));


            return context;
        }
    }
}
