using System.Drawing;
using System.Threading.Tasks;
using MultithreadingImageProcessing;

namespace ParallelExample
{
    public class ParallelInvokeExample
    {
        public string pathToFile1 { get; set; }
        public string pathToFile2 { get; set; }
        
        public void InvokeExample()
        {
            Parallel.Invoke(
                () => ImageProcessing.Dilation(new Bitmap(Image.FromFile(pathToFile1)),5),
                () => ImageProcessing.Dilation(new Bitmap(Image.FromFile(pathToFile2)),4)
                );
        }
    }
}