using System;
using Entites;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace Services
{
    public interface IHttpRequest
    {
        /// <summary>
        ///  HTTP Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string url);

        /// <summary>
        /// HTTP Post Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(string url, object body);

        /// <summary>
        /// HTTP Put Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PutAsync(string url, object body);

        /// <summary>
        /// HTTP Delete Request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync(string url);
    }
}
