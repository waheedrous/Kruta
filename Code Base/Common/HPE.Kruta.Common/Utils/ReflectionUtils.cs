using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace HPE.Kruta.Common.Utils
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Gets all custom attributes on a field set of the specified type using reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T[] GetAttributes<T>(MemberInfo info, bool inherit) where T : System.Attribute
        {
            var attributes =
                (T[])info.GetCustomAttributes(
                typeof(T), inherit);
            return attributes;
        }

        /// <summary>
        /// foreaches the properties of the closed type and builds a list of DataColumns 
        ///   from the name and type of the property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<DataColumn> DataColumns<T>()
        {
            var columns = new List<DataColumn>();

            // Nullables are special.  They should be treated as value types.
            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                return columns;

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    columns.Add(new DataColumn(info.Name, info.PropertyType.GetGenericArguments()[0]));
                }
                else
                {
                    columns.Add(new DataColumn(info.Name, info.PropertyType));
                }

            }
            foreach (FieldInfo info in typeof(T).GetFields())
            {
                if (info.IsStatic)
                    continue;

                if (info.FieldType.IsGenericType && info.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    columns.Add(new DataColumn(info.Name, info.FieldType.GetGenericArguments()[0]));
                }
                else
                {
                    columns.Add(new DataColumn(info.Name, info.FieldType));
                }
            }
            return columns;
        }
    }
}
