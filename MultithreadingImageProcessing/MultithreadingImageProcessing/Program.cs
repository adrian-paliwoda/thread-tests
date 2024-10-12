using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace MultithreadingImageProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var path =
                @"..\..\..\exampleImage.png";
            var pathNew =
                @"..\..\..\exampleImage2.png";

            var bitmap = new Bitmap(Image.FromFile(path));
            var result = ImageProcessing.Dilation(bitmap, 3);

            result.Save(pathNew);
        }
    }
}