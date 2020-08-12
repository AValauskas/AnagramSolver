using System;
using System.ComponentModel;

namespace Structures
{
    public class Generics
    {
        public static T MapValueToEnum<T,E>(E value) where T : struct
        {            
            T result = default(T);
            if (!Enum.TryParse(value.ToString(), out result))
                throw new Exception($"Value '{value}' is not part of {typeof(T).Name.ToString()} enum");

            return result;
                                
        }
    }

    //var converter = TypeDescriptor.GetConverter(typeof(T));
    //        try
    //        {
    //            var result = (T)converter.ConvertFrom(value.ToString());
    //            return result;
    //        }
    //        catch (Exception)
    //        {
    //            throw new Exception($"Value '{value}' is not part of {typeof(T).Name.ToString()} enum");
    //        }  
}
