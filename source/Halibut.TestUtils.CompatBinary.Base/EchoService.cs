using System;
using System.IO;
using System.Threading;

namespace Halibut.TestUtils.SampleProgram.v5_0_429
{
    public class EchoService : IEchoService
    {
        public string SayHello(string name)
        {
            return name;
        }

        public bool Crash()
        {
            throw new Exception();
        }

        public static Action OnLongRunningOperation { get; set; }

        public int LongRunningOperation()
        {
            OnLongRunningOperation();
            Thread.Sleep(10000);
            return 12;
        }

        public int CountBytes(DataStream stream)
        {
            var tempFile = Path.GetFullPath(Guid.NewGuid().ToString());
            stream.Receiver().SaveTo(tempFile);
            var length = (int) new FileInfo(tempFile).Length;
            File.Delete(tempFile);
            return length;
        }
    }
}