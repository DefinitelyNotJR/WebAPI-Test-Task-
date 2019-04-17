using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIUtils
{

    public static class HttpRequestUtils
    {
        public static async Task<String> GetAsync(string url)
        {
            string result = "";
            try
            {
                //Getting data from Uri 
                using (var client = new HttpClient())
                {
                    var resp = await client.GetAsync(url);
                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception();
                    }
                    if (resp.Content == null)
                    {
                        throw new NullReferenceException();
                    }
                    result = await resp.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
    }
}
