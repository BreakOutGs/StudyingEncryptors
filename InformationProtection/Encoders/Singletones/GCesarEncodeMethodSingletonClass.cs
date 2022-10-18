using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationProtection.Encoders;

namespace InformationProtection
{
    public partial class GCesarEncodeMethodClass: IEncoder
    {
        private static volatile GCesarEncodeMethodClass instance;
        private static object syncRoot = new Object();
        static int instanceCounter;


        public static GCesarEncodeMethodClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GCesarEncodeMethodClass();
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
