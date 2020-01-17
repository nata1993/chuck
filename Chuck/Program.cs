using System;
using System.Collections.Generic;
using static System.Console;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chuck
{
    class Program
    {
        static void Main()
        {
            string categoryURL = "https://api.chucknorris.io/jokes/categories";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryURL);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream)) 
            {
                var response = responseReader.ReadToEnd();
                WriteLine(response);
            }
        }
    }
}
