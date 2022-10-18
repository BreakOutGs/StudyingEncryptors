

using InformationProtection.Encoders;

namespace InformationProtection
{
    public partial class GPolibiyEncoderClass : IEncoder
    {
        private static volatile GPolibiyEncoderClass instance;
        private static object syncRoot = new Object();
        static int instanceCounter;


        public static GPolibiyEncoderClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GPolibiyEncoderClass();
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
