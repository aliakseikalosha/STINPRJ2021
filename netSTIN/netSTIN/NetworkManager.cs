using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace netSTIN
{
    public class NetworkManager
    {
        public static void Get(string url, Action<string> onSuccess, Action<string> onError)
        {
            new Task(() => GetRequest(url, (w) =>
              {
                  if (w.IsSuccessStatusCode)
                  {
                      onSuccess?.Invoke(w.Content.ReadAsStringAsync().Result);
                  }
                  else
                  {
                      Debug.LogError($": HTTP Error: {w.StatusCode}");
                      onError?.Invoke($"{url} get error with state {w.StatusCode}");
                  }
              })).Start();
        }

        public static void Get(string url, Action<HttpResponseMessage> onDownload)
        {
            new Task(() => GetRequest(url, onDownload)).Start();
        }

        private static async void GetRequest(string uri, Action<HttpResponseMessage> onDownload)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            var pageContents = await response.Content.ReadAsStringAsync();
            onDownload?.Invoke(response);
        }
    }
}