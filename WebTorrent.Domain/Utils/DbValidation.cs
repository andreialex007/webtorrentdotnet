using System;
using System.Data.Entity.Validation;
using System.Linq.Expressions;

namespace WebTorrent.Domain.Utils
{
    public static class DbValidation
    {
        public static DbValidationError ErrorFor<TSource>(Expression<Func<TSource, object>> propertyLambda, string errorText) where TSource : new()
        {
            var propertyName = new TSource().GetPropertyName(propertyLambda);
            return new DbValidationError(propertyName, errorText);
        }
    }
}
