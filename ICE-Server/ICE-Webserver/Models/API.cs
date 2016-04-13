using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ICE_Webserver.Models
{
    public class API
    {
        private string host = "http://localhost:6465/";
        private string accessToken;

        public API()
        {
            if (HttpContext.Current.Session["AccessToken"] != null)
            {
                accessToken = HttpContext.Current.Session["AccessToken"].ToString();
            }
        }

        
        /// <summary>
        /// Does a request to the web api
        /// </summary>
        /// <param name="method">The used HttpMethod for the request (for example GET/POST...)</param>
        /// <param name="uri">The url to which the request will be done</param>
        /// <returns>A response message that contains the data from the request</returns>
        public async Task<HttpResponseMessage> Request(HttpMethod method, string uri)
        {
            return await Request(method, uri, null);
        }

        /// <summary>
        /// Does a request to the web api
        /// </summary>
        /// <param name="method">The used HttpMethod for the request (for example GET/POST...)</param>
        /// <param name="uri">The url to which the request will be done</param>
        /// <param name="id">The id that is to be added after the request</param>
        /// <returns>A response message that contains the data from the request</returns>
        public async Task<HttpResponseMessage> Request(HttpMethod method, string uri, int id)
        {
            uri += "/" + id;

            return await Request(method, uri, null);
        }

        /// <summary>
        /// Does a request to the web api
        /// </summary>
        /// <param name="method">The used HttpMethod for the request (for example GET/POST...)</param>
        /// <param name="uri">The url to which the request will be done</param>
        /// <param name="id">The id that is to be added after the request</param>
        /// <param name="object">The object that will be posted</param>
        /// <returns>A response message that contains the data from the request</returns>
        public async Task<HttpResponseMessage> Request(HttpMethod method, string uri, int id, object @object)
        {
            uri += "/" + id;

            return await Request(method, uri, @object);
        }

        /// <summary>
        /// Does a request to the web api
        /// </summary>
        /// <param name="method">The used HttpMethod for the request (for example GET/POST...)</param>
        /// <param name="uri">The url to which the request will be done</param>
        /// <param name="object">The object that will be posted</param>
        /// <returns>A response message that contains the data from the request</returns>
        public async Task<HttpResponseMessage> Request(HttpMethod method, string uri, object @object)
        {
            using (var client = ConfigureClient())
            {
                var request = new HttpRequestMessage(method, uri);

                if (@object != null)
                {
                    // Convert the object to json
                    var objectJson = await JsonConvert.SerializeObjectAsync(@object);

                    // Set the post content
                    request.Content = new StringContent(objectJson);

                    // Set the type that will be posted (json)
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                return await client.SendAsync(request);
            }
        }

        /// <summary>
        ///     Sets the necessary parameters for the http client that does the request to the api
        /// </summary>
        private HttpClient ConfigureClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            SetClientAuthentication(client);

            return client;
        }

        /// <summary>
        ///     Adds a header to the http client containing the access token
        /// </summary>
        private void SetClientAuthentication(HttpClient client)
        {
            if (accessToken != null)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }
    }
}