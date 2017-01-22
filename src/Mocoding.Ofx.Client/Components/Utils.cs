using System;
using System.Linq;
using System.Text;
using Mocoding.Ofx.Client.Interfaces;

namespace Mocoding.Ofx.Client.Components
{
    class Utils : IUtils
    {
        public const string DateTimeFormat = "yyyyMMddHHmmss";

        public string GetCurrentDateTime()
        {
            return DateTime.Now.ToString(DateTimeFormat);
        }

        public string GenerateTransactionId()
        {
            return Guid.NewGuid().ToString();
        }

        public string DateToString(DateTime dateTime)
        {
            return dateTime.ToString(DateTimeFormat);
        }

        public string GetClientUid(string userId)
        {
            var bytes = Encoding.ASCII.GetBytes(userId + "chasebanksucks!").Take(16).ToArray();
            return new Guid(bytes).ToString("N");
        }
    }
}
