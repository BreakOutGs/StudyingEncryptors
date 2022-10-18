using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection.Encoders
{
    public partial class GBigramEncoderClass : IEncoder
    {
        private static volatile GBigramEncoderClass instance;
        private static object syncRoot = new Object();
        static int instanceCounter;


        public static GBigramEncoderClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GBigramEncoderClass();
                    }
                }

                return instance;
            }
        }

        public int getCounter()
        {
            return instanceCounter++;
        }

    }
}
