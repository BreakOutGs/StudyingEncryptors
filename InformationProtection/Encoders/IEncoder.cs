using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection.Encoders
{
    internal interface IEncoder
    {
        public string Encode(string textToEncode);

        public string Decode(string textToDecode);

    }
}
