using System;
using System.Net;
using System.Net.Mime;
using System.Threading;

namespace MultithreadingWaitingForTask
{
    class Program
    {
        static void Main(string[] args)
        {
//            DownloadFile();
//           DownloadFileAsync();
//            DownloadFileWithThread();
        }

        static void DownloadFile()
        {
            var remoteUri = "http://files2.codecguide.com/";
            var fileName = "K-Lite_Codec_Pack_1504_Full.exe";
            
            var myStringWebResource = String.Concat(remoteUri, fileName);
            
            WebClient myWebClient = new WebClient();
            Console.WriteLine("Starting download....");
            myWebClient.DownloadFile(myStringWebResource,fileName);
            Console.WriteLine("Downloading finished.");
        }
        
        static void DownloadFileWithThread()
        {
            var remoteUri = "http://files2.codecguide.com/";
            var fileName = "K-Lite_Codec_Pack_1504_Full.exe";
            
            var myStringWebResource = String.Concat(remoteUri, fileName);
            
            WebClient myWebClient = new WebClient();
            Console.WriteLine("Starting download....");
            
            var downloadThread = new Thread(() => myWebClient.DownloadFile (myStringWebResource, fileName));
            downloadThread.Start();
            

            while (downloadThread.IsAlive)
            {
                Console.WriteLine("Do something else...");
                Thread.Sleep(1000);
            }

            downloadThread.Join();
            Console.WriteLine("Downloading finished.");
        }
        
        static void DownloadFileAsync()
        {
            var remoteUri = "http://files2.codecguide.com/";
            var fileName = "K-Lite_Codec_Pack_1504_Full.exe";
            
            var myStringWebResource = String.Concat(remoteUri, fileName);
            
            WebClient myWebClient = new WebClient();
            Console.WriteLine("Starting download....");
            
            var downloadTask = myWebClient.DownloadFileTaskAsync(myStringWebResource, fileName);

            while (!downloadTask.IsCompleted)
            {
                Console.WriteLine("Do something else...");
                Thread.Sleep(1000);
            }
            
            Console.WriteLine("Downloading finished.");
        }
        
    }
}