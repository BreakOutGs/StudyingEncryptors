using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection.Encoders
{
    public partial class GWernamEncoderClass : IEncoder
    {
        private static volatile GWernamEncoderClass instance;
        private static object syncRoot = new Object();
        static int instanceCounter;


        public static GWernamEncoderClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GWernamEncoderClass();
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
