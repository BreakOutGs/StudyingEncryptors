using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection.Encoders
{
    public partial class GDESEncoderClass : IEncoder
    {
        private static volatile GDESEncoderClass instance;
        private static object syncRoot = new Object();
        static int instanceCounter;


        public static GDESEncoderClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GDESEncoderClass();
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
