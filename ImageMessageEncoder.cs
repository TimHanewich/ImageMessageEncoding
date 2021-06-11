using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

namespace ImageMessageEncoding
{
    public class ImageMessageEncoder
    {
        public Stream EncodeMessage(string msg)
        {
            
            byte[] bytes = Encoding.ASCII.GetBytes(msg);

            //Get the dimensions
            float bytecount = Convert.ToSingle(bytes.Length);
            int dimension = Convert.ToInt32(Math.Ceiling(Math.Sqrt(bytecount)));

            Bitmap bm = new Bitmap(dimension, dimension);
            
            //Counters
            int x = 0;
            int y = 0;
            
            foreach (byte b in bytes)
            {
                Color c = Color.FromArgb(Convert.ToInt32(b), Convert.ToInt32(b), Convert.ToInt32(b));
                bm.SetPixel(x, y, c);

                //If we are at the very last one now, go to the next line for next time
                if ((x + 1) == dimension)
                {
                    y = y + 1;
                    x = 0;
                }
                else
                {
                    x = x + 1;
                }
            }

            MemoryStream ms = new MemoryStream();
            bm.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms;
        }

        public string DecodeMessage(Stream s)
        {
            Image i = Image.FromStream(s);
            Bitmap bm = new Bitmap(i);
            
            //Get a list of all bytes
            List<byte> AllBytes = new List<byte>();
            for (int y = 0; y < i.Height; y++)
            {
                for (int x = 0; x < i.Width; x++)
                {
                    Color c = bm.GetPixel(x, y);
                    
                    //All 3 colors have to match for this to be added to the list
                    if ((c.R == c.G) && (c.G == c.B))
                    {
                        AllBytes.Add(c.R); //Could add any of them.
                    }
                    else
                    {
                        Console.WriteLine("Nope!");
                    }
                }
            }

            //Decode the bytes
            string msg = ASCIIEncoding.ASCII.GetString(AllBytes.ToArray());
            return msg;
        }



        private class ByteArrayHelper
        {
            private byte[] Bytes {get; set;}
            private int OnIndex {get; set;}

            public ByteArrayHelper(byte[] arr)
            {
                Bytes = arr;
                OnIndex = 0;
            }

            public byte NextByte()
            {
                OnIndex = OnIndex + 1;
                return Bytes[OnIndex - 1]; //Subtract one because we just incremented.
            }
        }
    }
}