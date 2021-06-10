using System;
using System.Linq.Expressions;
using System.Reflection;

namespace TrackerUI.Core.Helpers {

    public static class ExpressionHelpers {

        /// <summary>Compiles an expression and gets the functions return value</summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="lambda">The expression to compile</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda) => lambda.Compile().Invoke();

        /// <summary>Sets the underlying properties value to the given value from an expression that contains the property</summary>
        /// <typeparam name="T">The type of value to set</typeparam>
        /// <param name="lambda">The expression</param>
        /// <param name="value">The value to set the property to</param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value) {
            if (!(lambda?.Body is MemberExpression expression)) {
                return;
            }

            var propertyInfo = (PropertyInfo)expression.Member;

            object target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            propertyInfo.SetValue(target, value);
        }

    }

}