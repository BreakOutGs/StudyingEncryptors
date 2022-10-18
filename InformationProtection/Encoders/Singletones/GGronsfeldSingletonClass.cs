
using InformationProtection.Encoders;

namespace InformationProtection
{
    public partial class GGronsfeldEncoderClass : IEncoder
    {
        private static volatile GGronsfeldEncoderClass instance;
        private static object syncRoot = new Object();
        static int instanceCounter;


        public static GGronsfeldEncoderClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GGronsfeldEncoderClass();
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
