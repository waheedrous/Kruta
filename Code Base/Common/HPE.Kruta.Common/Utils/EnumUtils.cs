using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Common.Utils
{
    public static class EnumUtils
    {

        #region Get Description
        /// <summary>
        /// Uses <see cref="System.Reflection.MemberInfo.GetCustomAttributes"/> to get the [Description ("Some Message")] attribute of an Enum item (if any).
        /// <br />
        /// DescriptionAttribute is AllowMultiple=False, so there will only ever be 0 or 1. 
        /// <br />
        /// Thus, this method is the most expedient way of accessing the information.
        /// </summary>
        /// <param name="item">Enum</param>
        /// <returns>The DescriptionAttribute of the enum item, or the item's name if no DescriptionAttribute exists</returns>
        public static string GetDescription(Enum item)
        {
            string[] attributes = GetDescriptionArray(item);
            return (attributes.Length > 0) ? attributes[0] : item.ToString();
        }
        /// <summary>
        /// Uses <see cref="System.Reflection.MemberInfo.GetCustomAttributes"/> to get the [Description ("Some Message")] attribute of an Enum item (if any).
        /// </summary>
        /// <remarks>
        /// DescriptionAttribute is AllowMultiple=False, so there will only ever be 0 or 1.
        /// 
        /// However, it might be useful to have access to the natural string array returned from MemberInfo.GetCustomAttributes
        /// for other purposes and this method exposes that.
        /// </remarks>
        /// <param name="item">Enum</param>
        /// <returns>The DescriptionAttribute of the enum item, or the item's name if no DescriptionAttribute exists</returns>
        public static string[] GetDescriptionArray(Enum item)
        {
            ///Get a Reflection.FieldInfo from the type of the Enum
            var fi = item.GetType().GetField(item.ToString());
            ///DO NOT INHERIT; we want only the attributes of the Enum item itself
            var attributes = GetAttributes(fi, false);

            var iLength = (attributes.Length > 0) ? (attributes.Length) : 1;
            var retVal = new string[iLength];

            if (attributes.Length > 0)
            {
                for (int i = 0; i < attributes.Length; i++)
                {
                    retVal[i] = attributes[i].Description;
                }
            }
            else
            {
                retVal[0] = item.ToString();
            }
            return retVal;
        }

        /// <summary>
        /// Uses ComponentModel to get [Description ("Some Message")] attribute
        /// of an Enum item (if any)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="inherit"></param>
        /// <returns>All DescriptionAttribute of the enum, if any</returns>
        public static DescriptionAttribute[] GetAttributes(MemberInfo info, bool inherit)
        {
            return GetAttributes<DescriptionAttribute>(info, inherit);
        }

        /// <summary>
        /// Delegates to <see cref="M:Kruta.Common.Utils.ReflectionUtils.GetAttributes"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T[] GetAttributes<T>(MemberInfo info, bool inherit) where T : System.Attribute
        {
            return ReflectionUtils.GetAttributes<T>(info, inherit);
        }
        #endregion

        #region Get Name
        /// <summary>
        /// given a int value for an enum, returns the string equivalent
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <param name="defaultName">name to return if value does not exist in enum</param>
        /// <returns></returns>
        public static string GetName(Type enumType, int value, string defaultName)
        {
            try
            {
                return Enum.GetName(enumType, value);
            }
            catch
            {
                return defaultName;
            }
        }
        /// <summary>
        /// Generic version of GetName()
        /// given a int value for an enum, returns the string equivalent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultName">name to return if value does not exist in enum</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Only enums are supported</exception>
        public static string GetName<T>(int value, string defaultName)
        {
            try
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid Type: {0}", typeof(T).ToString()));
                return Enum.GetName(typeof(T), value);
            }
            catch
            {
                return defaultName;
            }
        }
        #endregion


        #region Parse

        /// <summary>
        /// A generic version of Enum.Parse()
        /// </summary>
        /// <example>
        /// EnumUtil.Parse&lt;SomeEnum&gt;someString)
        /// </example>
        /// <remarks>
        /// the standard way to do this is much more wordy:
        /// (SomeEnum)Enum.Parse(typeof(SomeEnum),someString)
        /// </remarks>
        /// <typeparam name="T">Your Enum</typeparam>
        /// <param name="text">the label to parse</param>
        /// <returns>Enum item of type T or Error</returns>
        /// <exception cref="EnumParseException">Unable to Parse the values and no default (0) field is defined in the Enum</exception>
        public static T Parse<T>(string text)
        {
            try
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid Type: {0}", typeof(T).ToString()));

                T t = (T)Enum.Parse(typeof(T), text, true);
                return t;
            }
            catch (Exception ex)
            {
                //throw new EnumParseException("Parse Failed", ex);
                throw ex;
            }
        }
        /// <summary>
        /// Passes thru to the string input overload
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T Parse<T>(int input)
        {
            return Parse<T>(input.ToString(CultureInfo.InvariantCulture));
        }
        /// <summary>
        /// Attempts to parse a given string value into an Enum field
        /// </summary>
        /// <typeparam name="T">the specific enum to return</typeparam>
        /// <param name="text">value to attempt to parse</param>
        /// <param name="defaultText">value to use if initial value is not found in Enum's fields</param>
        /// <returns>enum field value</returns>
        public static T Parse<T>(string text, string defaultText)
        {
            try
            {
                return Parse<T>(text);
            }
            catch
            {
                return Parse<T>(defaultText);
            }
        }
        /// <summary>
        /// Attempts to parse a given integer value into an Enum field
        /// </summary>
        /// <typeparam name="T">the specific enum to return</typeparam>
        /// <param name="input">value to attempt to parse</param>
        /// <param name="defaultValue">value to use if initial value is not found in Enum's fields</param>
        /// <returns>enum field value</returns>
        /// <exception cref="EnumParseException">Unable to Parse the values and no default (0) field is defined in the Enum</exception>
        public static T Parse<T>(int input, int defaultValue)
        {
            try
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid Type: {0}", typeof(T).ToString()));
                if (!Enum.IsDefined(typeof(T), input))
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} does not define a field equivalent to {1}", typeof(T).ToString(), input.ToString(CultureInfo.InvariantCulture)));
                return Parse<T>(input.ToString(CultureInfo.InvariantCulture));
            }
            catch
            {
                if (Enum.IsDefined(typeof(T), defaultValue))
                    return Parse<T>(defaultValue.ToString(CultureInfo.InvariantCulture));

                if (default(T) != null)
                    return default(T);

                //throw new EnumParseException(string.Format(CultureInfo.InvariantCulture, "Unable to Parse {0}", typeof(T)));
                throw new Exception(string.Format(CultureInfo.InvariantCulture, "Unable to Parse {0}", typeof(T)));
            }
        }

        /// <summary>
        /// Attempts to parse a given integer value into an Enum field
        /// </summary>
        /// <typeparam name="T">the specific enum to return</typeparam>
        /// <param name="input">value to attempt to parse</param>
        /// <param name="defaultValue">value to use if initial value is not found in Enum's fields</param>
        /// <returns>enum field value</returns>
        /// <exception cref="EnumParseException">Unable to Parse the values and no default (0) field is defined in the Enum</exception>
        public static T Parse<T>(int input, T defaultValue)
        {
            try
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid Type: {0}", typeof(T).ToString()));
                if (!Enum.IsDefined(typeof(T), input))
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} does not define a field equivalent to {1}", typeof(T).ToString(), input.ToString(CultureInfo.InvariantCulture)));
                return Parse<T>(input.ToString(CultureInfo.InvariantCulture));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool TryParse<T>(string text, out T output)
        {
            try
            {
                output = Parse<T>(text);
                return true;
            }
            catch
            {
                output = default(T);
                return false;
            }
        }
        public static bool TryParse<T>(int? value, out T output)
        {
            if (!value.HasValue)
            {
                output = default(T);
                return false;
            }

            try
            {
                output = Parse<T>(value.GetValueOrDefault());
                return true;
            }
            catch
            {
                output = default(T);
                return false;
            }
        }

        #endregion

        #region Array Treatment
        /// <summary>
        /// Determines the number of fields in the Enum
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <returns>count of fields in the Enum</returns>
        public static int Length<T>()
        {
            return typeof(T).IsEnum ? Enum.GetValues(typeof(T)).GetLength(0) : 0;
        }

        /// <summary>
        /// Determines the highest Integer value of the enum's fields
        /// </summary>
        /// <typeparam name="T">Int32 based Enum</typeparam>
        /// <returns>Highest Int32 value of the Enum's fields</returns>
        /// <exception cref="EnumUnderlyingTypeException">Only Int32 based Enums are supported</exception>
        public static int MaxValue<T>()
        {
            if (typeof(T).IsEnum)
            {
                try
                {
                    if (Enum.GetUnderlyingType(typeof(T)) != typeof(Int32))
                        //throw new EnumUnderlyingTypeException(String.Format(CultureInfo.InvariantCulture, "This method expects an Int32 based Enum, not a {0} based Enum", Enum.GetUnderlyingType(typeof(T))));
                        throw new Exception(String.Format(CultureInfo.InvariantCulture, "This method expects an Int32 based Enum, not a {0} based Enum", Enum.GetUnderlyingType(typeof(T))));

                    var maxVal = 0;
                    foreach (var item in Enum.GetValues(typeof(T)))
                    {
                        var field = Parse<T>(item.ToString());
                        var itemValue = Convert.ToInt32(field, CultureInfo.InvariantCulture);
                        if (itemValue > maxVal)
                            maxVal = itemValue;

                    }
                    return maxVal;


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return 0;
        }
        /// <summary>
        /// Determines the lowest Integer value of the enum's fields
        /// </summary>
        /// <typeparam name="T">Int32 based Enum</typeparam>
        /// <returns>Lowest Int32 value of the Enum's fields</returns>
        /// <exception cref="EnumUnderlyingTypeException">Only Int32 based Enums are supported</exception>
        public static int MinValue<T>()
        {
            if (typeof(T).IsEnum)
            {
                try
                {
                    if (Enum.GetUnderlyingType(typeof(T)) != typeof(Int32))
                        //throw new EnumUnderlyingTypeException(String.Format(CultureInfo.InvariantCulture, "This method expects an Int32 based Enum, not a {0} based Enum", Enum.GetUnderlyingType(typeof(T))));
                        throw new Exception(String.Format(CultureInfo.InvariantCulture, "This method expects an Int32 based Enum, not a {0} based Enum", Enum.GetUnderlyingType(typeof(T))));

                    var minVal = 0;
                    foreach (var item in Enum.GetValues(typeof(T)))
                    {
                        var field = Parse<T>(item.ToString());
                        var itemValue = Convert.ToInt32(field, CultureInfo.InvariantCulture);
                        if (itemValue < minVal)
                            minVal = itemValue;

                    }
                    return minVal;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return 0;
        }
        #endregion


    }
}
