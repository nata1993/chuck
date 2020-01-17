using System;
using System.Collections.Generic;
using static System.Console;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nancy.Json;

namespace Chuck
{
    class Joke
    {
        public string Value { get; set; }
    }
    class Program
    {
        static void Main()
        {
            GetCategory();
            GetRandomJoke();
        }

        public static void GetRandomJoke()
        {
            string randomJokeURL = "https://api.chucknorris.io/jokes/random";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(randomJokeURL);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                WriteLine("\n" + randomJoke.Value);
            }
        }
        public static void GetCategory()
        {
            string categoryURL = "https://api.chucknorris.io/jokes/categories";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryURL);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd(); //unserialized response

                //serialize response
                /*JavaScriptSerializer ser = new JavaScriptSerializer();
                var categories = ser.Deserialize<List<string>>(response);
                foreach (string cat in categories)
                {
                    WriteLine(cat);
                }*/

                //serialize alternative method
                string[] category = response.Split(new char[] { ',', ' ', '[', ']', '"' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < category.Length; i++)
                {
                    WriteLine(category[i]);
                }
            }
        }
        public static void SearchCategory(string[] cat, string input)
        {
            WriteLine("\nEnter your search\n");
            string userInput = ReadLine();

            for (int i = 0; i < cat.Length; i++)
            {
                if (input.ToLower() == cat[i])
                {
                    WriteLine($"Found category: {cat[i]}");
                }

                else if(input.ToLower() != cat[i])
                    WriteLine("Not found such category");
            }
        }
    }
}
