using System;
using System.ComponentModel;

namespace Structures
{
    public class Generics
    {
        public static T MapValueToEnum<T,E>(E value) where T : struct
        {            
            T result;
            if (!Enum.IsDefined(typeof(T), value))
            {
                throw new Exception($"Value '{value}' is not part of {typeof(T).Name.ToString()} enum");
            }
            result = (T)Enum.Parse(typeof(T), value.ToString());
            
            return result;                                
        }
    }
}
