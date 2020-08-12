using System;
using System.ComponentModel;

namespace Structures
{
    public enum Gender : int
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
    public enum Weekday
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public class Generics<T, E>
    {
        public static T MapValueToEnum(E value)
        {            
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {               
                var result = (T)converter.ConvertFrom(value);
                return result;
            }
            throw new Exception($"Value '{value}' is not part of Gender enum");          
        }
    }
}
