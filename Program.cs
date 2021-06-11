using System;
using System.IO;

namespace ImageMessageEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            // string msg = System.IO.File.ReadAllText(@"C:\Users\tahan\Downloads\bible.txt");
            // ImageMessageEncoder enc = new ImageMessageEncoder();
            // Stream s = enc.EncodeMessage(msg);
            
            // Stream twt = System.IO.File.OpenWrite(@"C:\Users\tahan\Downloads\MyMsgImg.png");
            // s.CopyTo(twt);
            // twt.Dispose();
            // s.Dispose();

            Stream s = System.IO.File.OpenRead(@"C:\Users\tahan\Downloads\MyMsgImg.png");
            ImageMessageEncoder enc = new ImageMessageEncoder();
            string msg = enc.DecodeMessage(s);
            Console.WriteLine(msg);
        }
    }
}
