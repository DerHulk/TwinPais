using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public class SimplePersistence
    {
        public string Path { get; }

        public SimplePersistence()
        {
            
            Path = System.AppContext.BaseDirectory  + "\\..\\App_Data\\";
        }

        public void Save<T>(T toSave, Func<T,bool> identifiyer)
        {
            var saveName = toSave.GetType().Name;
            var location = System.IO.Path.Combine(Path, saveName);
            var json = string.Empty;
            var array = this.Read<T>();

            if (array == null)
                array = new T[] { toSave };

            else
            {
                array = array.Where(x=> !identifiyer(x)).Union( new T[] { toSave }).ToArray();
            }

            json = Newtonsoft.Json.JsonConvert.SerializeObject(array); //NetJSON.NetJSON.Serialize(array);
            System.IO.File.WriteAllText(location, json);
        }

        public T[] Read<T>()
        {
            var locationName = typeof(T).Name;
            var location = System.IO.Path.Combine(Path, locationName);
            var json = string.Empty;

            if (System.IO.File.Exists(location))
            {
                json = System.IO.File.ReadAllText(location);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(json);//NetJSON.NetJSON.Deserialize<T[]>(json);
            }
            else
                return null;
        }
    }
}
