﻿using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Westwind.AspnetCore
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Retrieve the raw body as a string from the Request.Body stream
        /// </summary>
        /// <param name="request">Request instance to apply to</param>
        /// <param name="encoding">Optional - Encoding, defaults to UTF8</param>
        /// <param name="inputStream">Optional - Pass in the stream to retrieve from. Other Request.Body</param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null, Stream inputStream = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            if (inputStream == null)
                inputStream = request.Body;

            using (StreamReader reader = new StreamReader(inputStream, encoding))
                return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Retrieves the raw body as a byte array from the Request.Body stream
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request, Stream inputStream = null)
        {
            if (inputStream == null)
                inputStream = request.Body;

            using (var ms = new MemoryStream(2048))
            {
                await inputStream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}