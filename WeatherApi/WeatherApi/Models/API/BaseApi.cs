using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi.Models.API
{
    public class BaseApi<T>
    {
        protected string url { get; set; }

        protected IDictionary<string, string> getParameters;

        protected IDictionary<string, string> postParameters;

        protected IDictionary<string, string> headers;

        public BaseApi(string url)
        {
            this.url = url;
            getParameters = new Dictionary<string, string>();

            postParameters = new Dictionary<string, string>();

            headers = new Dictionary<string, string>();
        }

        protected async Task<T> sendGetReqAndDeserialize()
        {

            HttpRequestMessage requestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    url + "?" + getParametersToString()
                    );

            addHeaders(requestMessage);

            return await sendRequset(requestMessage);

        }

        protected async Task<T> sendPostRequestAndDeserialize()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(
                    HttpMethod.Post, url);

            requestMessage.Content = new FormUrlEncodedContent(postParameters);

            addHeaders(requestMessage);

            return await sendRequset(requestMessage);

        }

        
        private string getParametersToString()
        {
            string paras = "";

            foreach (KeyValuePair<string, string> para in getParameters)
                paras += para.Key + "=" + para.Value + "&";


            if (paras.EndsWith("&"))
                paras = paras.Remove(paras.Length - 1, 1);

            return paras;
        }

        private void addHeaders(HttpRequestMessage request)
        {
            foreach (KeyValuePair<string, string> header in headers)
                request.Headers.Add(header.Key, header.Value);
        }


        private static async Task<T> sendRequset(HttpRequestMessage request)
        {
            HttpClient client = new HttpClient();

            using (HttpResponseMessage response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>
                     (await response.Content.ReadAsStringAsync());
            }

        }

    }
}
