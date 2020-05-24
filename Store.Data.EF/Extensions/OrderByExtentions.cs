﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{

    public static class OrderByExtentions

    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)

        {
            return (IQueryable<T>)OrderBy((IQueryable)source, propertyName);
        }

        public static IQueryable OrderBy(this IQueryable source, string propertyName)

        {
            var x = Expression.Parameter(source.ElementType, "x");
            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);
            return source.Provider.CreateQuery(
                Expression.Call(typeof(Queryable), "OrderBy", new Type[] { source.ElementType, selector.Body.Type },
                     source.Expression, selector
                     ));
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)

        {
            return (IQueryable<T>)OrderByDescending((IQueryable)source, propertyName);
        }

        public static IQueryable OrderByDescending(this IQueryable source, string propertyName)

        {
            var x = Expression.Parameter(source.ElementType, "x");

            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);

            return source.Provider.CreateQuery(

                Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { source.ElementType, selector.Body.Type },

                     source.Expression, selector

                     ));
        }
    }

}

