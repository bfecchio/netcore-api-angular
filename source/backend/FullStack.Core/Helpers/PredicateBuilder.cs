using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace FullStack.Core.Helpers
{
    public static class PredicateBuilder
    {
        #region Public Static Methods
 
        public static Expression<Func<T, bool>> True<T>()
            => param => true;

        public static Expression<Func<T, bool>> False<T>()
            => param => false;

        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
            => predicate;
 
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
            => first.Compose(second, Expression.AndAlso);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
            => first.Compose(second, Expression.OrElse);
  
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        #endregion

        #region Private Methods
 
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {            
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
            
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        #endregion

        #region Private Classes

        private class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> map;

            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
                => this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
                => new ParameterRebinder(map).Visit(exp);

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }

        #endregion
    }
}
