using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace api_gateway_core.Request
{
    public class Request
    {
        private readonly HttpClient _httpClient;

        public Request()
        {
            _httpClient = new HttpClient();
        }

        private StringContent ConvertToStringContent<T>(T form)
        {
            return new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json");
        }

        public HttpResponseMessage Get(string uri, string parameters = "")
        {
            try
            {
                return _httpClient.GetAsync($"{uri}{parameters}").Result;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }

        public HttpResponseMessage Post<T>(string uri, T form, string parameters = "")
        {
            try
            {
                return _httpClient.PostAsync("teste", ConvertToStringContent(form)).Result;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }

        public HttpResponseMessage Put<T>(string uri, T form, string parameters = "")
        {
            try
            {
                return _httpClient.PutAsync($"{uri}{parameters}", ConvertToStringContent(form)).Result;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }

        public HttpResponseMessage Delete(string uri, string parameters = "")
        {
            try
            {
                return _httpClient.DeleteAsync($"{uri}{parameters}").Result;

            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}