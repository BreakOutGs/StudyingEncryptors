
using InformationProtection.Encoders;

namespace InformationProtection
{
    internal partial class GReplacementEncodeMethodClass : IEncoder
    {
        private static volatile GReplacementEncodeMethodClass instance;
        private static object syncRoot = new Object();
        static int instanceCounter;

        public static GReplacementEncodeMethodClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GReplacementEncodeMethodClass();
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
