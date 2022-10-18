using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection.Encoders
{
    public partial class GRSAEncoderClass: IEncoder
    {
        private static volatile GRSAEncoderClass instance;
        private static object syncRoot = new Object();
        static int instanceCounter;


        public static GRSAEncoderClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GRSAEncoderClass();
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
