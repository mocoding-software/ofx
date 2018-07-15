using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Interfaces;

namespace Mocoding.Ofx.Client.Components
{
    /// <summary>
    /// The reason to have this class is that HttpClient for Linux is broken for our scenario
    /// It is based on curl and curl send Expect header which is not handled by Chase endpoint.
    /// As the result server responds with 417 Expectation Failed.
    /// 
    /// This class do http POST request using SSL to specific endpoint with the specific content. Easy.
    /// </summary>
    /// <seealso cref="IOfxClientTransport" />
    public class TcpClientTransport : IOfxClientTransport
    {
        public async Task<string> PostRequest(Uri url, string content)
        {
            var server = url.Host;
            var port = url.Port == 80 || url.Port == 443 ? string.Empty : ":" + url.Port;

            var builder = new StringBuilder();

            builder.AppendLine($"POST {url} HTTP/1.1");
            builder.AppendLine("Content-Type: application/x-ofx");
            builder.AppendLine($"Host: {server}{port}");
            builder.AppendLine($"Content-Length: {content.Length}");
            builder.AppendLine("Connection: Keep-Alive");
            builder.AppendLine();
            builder.Append(content);

            var httpRequest = builder.ToString();
            StringBuilder httpResponse = new StringBuilder();
            using (var client = new TcpClient())
            {
                await client.ConnectAsync(server, url.Port);
                if (url.Scheme == "https")
                {
                    using (var sslStream = new SslStream(client.GetStream()))
                    {
                        await sslStream.AuthenticateAsClientAsync(server);
                        var toSend = Encoding.ASCII.GetBytes(httpRequest);
                        sslStream.Write(toSend);
                        ReadResponse(client, sslStream, httpResponse);
                    }
                }
                else
                {
                    using (var stream = client.GetStream())
                    {
                        var toSend = Encoding.ASCII.GetBytes(httpRequest);
                        stream.Write(toSend, 0, toSend.Length);
                        ReadResponse(client, stream, httpResponse);
                    }
                }
            }
            var httpContent = httpResponse.ToString();
            var contentIndex = httpContent.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4;
            var endIndex = httpContent.LastIndexOf(">", StringComparison.Ordinal);
            return httpContent.Substring(contentIndex, endIndex - contentIndex + 1);
        }

        private static void ReadResponse(TcpClient client, Stream sslStream, StringBuilder httpResponse)
        {
            var chunk = string.Empty;
            do
            {
                var received = new byte[client.ReceiveBufferSize];
                var count = sslStream.Read(received, 0, client.ReceiveBufferSize);
                chunk = Encoding.ASCII.GetString(received.Take(count).ToArray());
                httpResponse.Append(chunk);
            } while (!chunk.Contains("</OFX>"));
        }
    }
}