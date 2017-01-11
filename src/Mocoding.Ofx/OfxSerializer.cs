using System;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx
{
    public class OfxSerializer
    {
        public const string Default103Header =
       @"OFXHEADER:100
DATA:OFXSGML
VERSION:103
SECURITY:NONE
ENCODING:USASCII
CHARSET:1252
COMPRESSION:NONE
OLDFILEUID:NONE
NEWFILEUID:NONE

";

        private readonly SgmlSerializer<OFX> _sgmlSerializer;

        public OfxSerializer()
        {
            _sgmlSerializer = new SgmlSerializer<OFX>();
        }

        public string Serialize(OFX request)
        {
            var sgml = _sgmlSerializer.Serialize(request);
            return Default103Header + sgml;
        }

        public OFX Deserialize(string responseBody)
        {
            var ofxDataStartIndex = responseBody.IndexOf("<OFX>", StringComparison.OrdinalIgnoreCase);
            if (ofxDataStartIndex == -1)
                throw new FormatException("<OFX> element is not present in the response body");
            var sgml = responseBody.Substring(ofxDataStartIndex);

            var result = _sgmlSerializer.Deserialize(sgml);

            return result;
        }
    }
}
