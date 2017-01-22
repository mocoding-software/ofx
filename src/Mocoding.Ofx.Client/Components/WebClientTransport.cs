using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Exceptions;
using Mocoding.Ofx.Client.Interfaces;

namespace Mocoding.Ofx.Client.Components
{
    /// <summary>
    /// Implementation of OfxClient using WebClient class.
    /// </summary>
    class WebClientTransport : IOfxClientTransport
    {
        public async Task<string> PostRequest(Uri url, string content)
        {
            string result = null;

            using (var client = new HttpClient() { })
            {
                client.DefaultRequestHeaders.ExpectContinue = false;
                var httpContent = new StringContent(content, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-ofx");
                var response = await client.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                    throw new OfxTransportException("Failed to send request to " + url);
            }
                
            return result;
        }
    }
}
