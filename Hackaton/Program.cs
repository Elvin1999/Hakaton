using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace Hackaton
{
    class Program
    {
        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
        static async void Request(Data mydata)
        {
            string CateJson = JsonConvert.SerializeObject(mydata);
            HttpClient Client = new HttpClient();
            string baseAddress = "http://complete.azincloud.az:8880/register";
            string Link = $"{baseAddress}?={CateJson}";
            var Request = new HttpRequestMessage(HttpMethod.Post, Link);
            HttpResponseMessage Message = await Client.SendAsync(Request).ConfigureAwait(false);
            try
            {
                Message.EnsureSuccessStatusCode();
                Console.WriteLine((int)Message.StatusCode);
            }
            catch (Exception)
            {
            }
        }
        static void Main(string[] args)
        {
            Data data = new Data()
            { };
            var hash = Hash("camalzade_elvin@mail.ru");
            data.Email = "camalzade_elvin@mail.ru";
            data.Key = hash;
            Config config = new Config();
            config.FileName = "data.json";
            config.Data = data;
            Data mydata = new Data();
            if (File.Exists(config.FileName))
            {
                mydata = config.DeserializeWordsFromJson();
                Console.WriteLine(mydata.Email);
                Console.WriteLine(mydata.Key);
            }
            else
            {
                config.SeriailizeWordsToJson();
            }
            Request(mydata);
        }
    }
}