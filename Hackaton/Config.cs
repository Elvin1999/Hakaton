using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hackaton
{
    class Config
    {
        public string FileName { get; set; }
        public Data Data { get; set; }

        public void SeriailizeWordsToJson()
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                var item = JsonConvert.SerializeObject(Data);
                sw.WriteLine(item);
            }
        }
        public Data DeserializeWordsFromJson()
        {
            try
            {
                var context = File.ReadAllText(FileName);
                Data = JsonConvert.DeserializeObject<Data>(context);
            }
            catch (Exception)
            {
            }
            return Data;
        }
    }
}
