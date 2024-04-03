using System.Linq.Expressions;

namespace Services
{
	public static class ApiService : object
    {
      
        #region public static IQueryable<T> WhereContains<T>(this IQueryable<T> query, string? search)
        public static IQueryable<T> WhereContains<T>(this IQueryable<T> query, string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return query;

            if (search.IndexOf(':') < 0)
                return query;

            var searchItem = search.Split(':');

            if (searchItem[0] == null || searchItem[1] == null)
                return query;

            return query.WhereContains<T>(searchItem[0], searchItem[1]);
        }
        #endregion /public static IQueryable<T> WhereContains<T>(this IQueryable<T> query, string? search)

        #region private static IQueryable<T> WhereContains<T>(this IQueryable<T> query, string propertyName, object contains)
        private static IQueryable<T> WhereContains<T>(this IQueryable<T> query, string propertyName, object contains)
        {
            if (contains == null)
                return query;

            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);

            switch (propertyExpression.Type.Name)
            {
                case "String":
                    var stringObject = (string)contains;
                    var containsExpression = Expression.Call(propertyExpression, "Contains", null, Expression.Constant(stringObject, typeof(string)));
                    return query.Where(Expression.Lambda<Func<T, bool>>(containsExpression, parameter));

                default:
                    var valueString = contains.ToString();
                    if (valueString == null)
                        return query;

                    var convertedObject = Convert.ChangeType(contains, propertyExpression.Type);
                    var equalsExpression = Expression.Equal(
                        Expression.PropertyOrField(parameter, propertyName),
                        Expression.Constant(convertedObject));

                    return query.Where(Expression.Lambda<Func<T, bool>>(equalsExpression, parameter));
            }
        }
        #endregion /private static IQueryable<T> WhereContains<T>(this IQueryable<T> query, string propertyName, object contains)

        #region public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string? propertyName, bool isASC)
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string? propertyName, bool isASC)
        {

            if (propertyName == null)
                return query;

            var propertyType = typeof(T).GetProperty(propertyName)?.PropertyType;

            if (propertyType == null)
                return query;

            if (isASC)
                return query.OrderBy<T>(propertyType, propertyName);
            else
                return query.OrderByDescending<T>(propertyType, propertyName);
        }
        #endregion /public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string? propertyName, bool isASC)

        #region private static IQueryable<T> OrderBy<T>(this IQueryable<T> query, Type propertyType, string propertyName)
        private static IQueryable<T> OrderBy<T>(this IQueryable<T> query, Type propertyType, string propertyName)
        {

            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(propertyExpression, new[] { parameter });

            var resultQuery = typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                                    .Single()
                                    .MakeGenericMethod(new[] { typeof(T), propertyType })
                                    .Invoke(null, new object[] { query, lambda }) as IOrderedQueryable<T>;

            if (resultQuery == null)
                return query;

            return resultQuery;
        }
        #endregion /private static IQueryable<T> OrderBy<T>(this IQueryable<T> query, Type propertyType, string propertyName)

        #region private static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, Type propertyType, string propertyName)
        private static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, Type propertyType, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(propertyExpression, new[] { parameter });

            var resultQuery = typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2)
                                    .Single()
                                    .MakeGenericMethod(new[] { typeof(T), propertyType })
                                    .Invoke(null, new object[] { query, lambda }) as IOrderedQueryable<T>;

            if (resultQuery == null)
                return query;

            return resultQuery;
        }
        #endregion /private static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, Type propertyType, string propertyName)

        #region public static string BarcodGenerator(this long factorId)
        public static string BarcodGenerator(this long factorId)
        {
            try
            {
                return $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm").Replace("-", "")}{factorId}";
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion /public static string BarcodGenerator(this long factorId)

        #region public static string BarcodGenerator(this int factorId)
        public static string BarcodGenerator(this int factorId)
        {
            try
            {
                return $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm").Replace("-", "")}{factorId}";
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion /public static string BarcodGenerator(this int factorId)
    }
}