using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WGK.Lib.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Generic TryParse almost-anything Extension method.
        /// </summary>
        /// <remarks>
        /// This method is expensive compared to using the Types' dedicated TryParse methods.
        /// </remarks>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="pObject">The object.</param>
        /// <returns></returns>
        public static T? TryParse<T>(this object pObject) where T : struct
        {
            // To use this extension method:
            // decimal? d = obj.TryParse<decimal>();
            // int? i = obj.TryParse<int>();
            // DateTime? dt = obj.TryParse<DateTime>();
            
            if (pObject == null)
            {
                return null;
            }

            T? vResult = default(T);
            TypeConverter vTypeConverter = TypeDescriptor.GetConverter(typeof(T));
            if (vTypeConverter != null)
            {
                try
                {
                    string vObjectAsString = pObject.ToString(); 
                    vResult = (T)vTypeConverter.ConvertFromString(vObjectAsString);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return vResult;
        }

        /// <summary>
        /// Creates a deep clone including all serializable members of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T pObject)
        {
            using (var vMemoryStream = new MemoryStream())
            {
                var vBinaryFormatter = new BinaryFormatter();
                vBinaryFormatter.Serialize(vMemoryStream, pObject);
                vMemoryStream.Position = 0;
                return (T)vBinaryFormatter.Deserialize(vMemoryStream);
            }
        }

        public static bool IsNumber(this object pObject)
        {
            if (pObject is int) return true;
            if (pObject is int?) return true;
            if (pObject is long) return true;
            if (pObject is long?) return true;
            if (pObject is double) return true;
            if (pObject is double?) return true;
            if (pObject is float) return true;
            if (pObject is float?) return true;
            if (pObject is decimal) return true;
            if (pObject is decimal?) return true;
            return false;
        }
    }
}
