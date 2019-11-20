using System;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Interfaces;

[assembly: InternalsVisibleTo("Mocoding.Ofx.Tests")]

namespace Mocoding.Ofx.Client.Discover
{
    /// <summary>
    /// Discover Credit Card specific implementation of protocol utils.
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Client.OfxProtocolUtils" />
    public class DiscoverProtocolUtils : OfxProtocolUtils
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoverProtocolUtils"/> class.
        /// </summary>
        /// <param name="requests">The requests.</param>
        public DiscoverProtocolUtils(IOfxRequestLocator requests) : base(requests)
        {

        }

        /// <summary>
        /// Gets the client uid.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Client ID
        /// </returns>
        public override string GetClientUid(string userId)
        {
            return null;
        }

        /// <summary>
        /// Creates and executes POST request to specified url with specified body content.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public override async Task<string> PostRequest(Uri url, string content)
        {
            var server = url.Host;
            var httpRequest = PrepareRequest(server, url.Port, content);
            StringBuilder httpResponse = new StringBuilder();
            using (var client = new TcpClient())
            {
                await client.ConnectAsync(server, url.Port);
                if (url.Scheme == "https")
                {
                    using (var sslStream = new SslStream(client.GetStream(), true))
                    {
                        await sslStream.AuthenticateAsClientAsync(server);
                        var toSend = Encoding.ASCII.GetBytes(httpRequest);
                        await sslStream.WriteAsync(toSend, 0, toSend.Length);
                        await sslStream.FlushAsync();
                        await ReadResponse(client, sslStream, httpResponse);
                    }
                }
                else
                {
                    using (var stream = client.GetStream())
                    {
                        var toSend = Encoding.ASCII.GetBytes(httpRequest);
                        stream.Write(toSend, 0, toSend.Length);
                        await ReadResponse(client, stream, httpResponse);
                    }
                }
            }
            var httpContent = httpResponse.ToString();
            var contentIndex = httpContent.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4;
            var endIndex = httpContent.LastIndexOf(">", StringComparison.Ordinal);
            return httpContent.Substring(contentIndex, endIndex - contentIndex + 1);
        }

        private static async Task ReadResponse(TcpClient client, Stream sslStream, StringBuilder httpResponse)
        {
            var chunk = string.Empty;
            if (sslStream.CanRead)
                do
                {
                    var received = new byte[client.ReceiveBufferSize];
                    var count = await sslStream.ReadAsync(received, 0, client.ReceiveBufferSize);
                    chunk = Encoding.ASCII.GetString(received.Take(count).ToArray());
                    httpResponse.Append(chunk);
                } while (!chunk.Contains("</OFX>"));
        }

        internal static string PrepareRequest(string server, int port, string content)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"POST / HTTP/1.1");
            builder.AppendLine("Content-Type: application/x-ofx");
            builder.AppendLine($"Host: {server}:{port}");
            builder.AppendLine($"Content-Length: {content.Length}");
            builder.AppendLine("Connection: close");
            // builder.AppendLine("User-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_4) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.1 Safari/605.1.15");
            // builder.AppendLine("Accept: */*");
            // builder.AppendLine("Accept-Language: en-us");
            // builder.AppendLine("Cache-Control: no-cache");
            
            builder.AppendLine();
            builder.Append(content);

            var httpRequest = builder.ToString();
            return httpRequest;
        }
    }
}
