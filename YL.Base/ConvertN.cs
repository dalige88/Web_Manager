using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace YL.Base
{
    public partial class ConvertN
    {
        /// <summary>
        /// 将指当前值转换为指定的类型<br/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToType<T>(object value)
        {
            Type t = typeof(T);
            //处理可空类型
            Type nullable = Nullable.GetUnderlyingType(t);
            if (nullable != null)
            {
                //如果传入的值是空引用或空字符串，返回默认值
                if (value == null || (value is string && string.IsNullOrEmpty(value as string)))
                {
                    return default(T);
                }
                t = nullable;
            }

            //转换
            object o = Convert.ChangeType(value, t.IsEnum ? Enum.GetUnderlyingType(t) : t);

            if (t.IsEnum && !HasFlagsAttribute(t) && !Enum.IsDefined(t, o)) throw new Exception("枚举类型\"" + t.ToString() + "\"中没有定义\"" + (o == null ? "null" : o.ToString()) + "\"");

            //可空类型
            if (nullable != null)
            {
                ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { nullable });
                return (T)constructor.Invoke(new object[] { o });
            }

            return (T)o;
        }
        /// <summary>
        /// 判断是否定义了FlagsAttribute属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasFlagsAttribute(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(FlagsAttribute), true);
            return attributes != null && attributes.Length > 0;
        }
    }
}
