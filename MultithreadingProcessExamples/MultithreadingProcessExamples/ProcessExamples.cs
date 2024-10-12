using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace MultithreadingProcessExamples
{
    internal static class ProcessExamples
    {
        public static void SimpleStart()
        {
            var pathToExecuteFile = "notepad";

            Process.Start(pathToExecuteFile);
        }

        public static void KillByName()
        {
            var pathToExecuteFile = "notepad";
            var processToKill = Process.Start(pathToExecuteFile);

            Thread.Sleep(1000);

            processToKill?.Kill();
        }

        public static void WaitForExit()
        {
            var pathToExecuteFile = "notepad";
            var processToKill = Process.Start(pathToExecuteFile);

            processToKill?.WaitForExit();
        }

        public static void UseProcessStartInfo()
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "notepad.exe";
            startInfo.Arguments = "exampleTextFile.txt";
            startInfo.WorkingDirectory = ".";
            startInfo.ErrorDialog = true;

            //Stworzenie przykładowego pliku tekstowego z zawartością
            CreateExampleTextFile(startInfo.Arguments);

            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void UseStreamOfProcessWithConnectionToDatabase()
        {
            var pathToDatabaseExecute = "osql";
            var query = "SELECT TOP 10 [name] FROM [master].[sys].[databases]";
            var dataBaseName = "master";
            var serverName = @"localhost\sqlexpress";

            var startProcessInfo = new ProcessStartInfo
            {
                FileName = pathToDatabaseExecute,
                Arguments = $"-E -S \"{serverName}\" -d \"{dataBaseName}\" -Q \"{query}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var process = new Process {StartInfo = startProcessInfo};
            process.Start();

            using (var processStandardOutput = process.StandardOutput)
            {
                Console.WriteLine("---- Beginning of query result ---- ");
                Console.WriteLine(processStandardOutput.ReadToEnd());
                Console.WriteLine("---- End of query result ---- ");
            }
        }

        public static void UseStreamOfProcessToFile()
        {
            var filePathTostreamEnd = "queryResult.txt";
            var pathToDatabaseExecute = "osql";
            var query = "SELECT [name] FROM [master].[sys].[databases]";
            var dataBaseName = "master";
            var serverName = @"localhost\sqlexpress";

            var startProcessInfo = new ProcessStartInfo
            {
                FileName = pathToDatabaseExecute,
                Arguments = $"-E -S \"{serverName}\" -d \"{dataBaseName}\" -Q \"{query}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var process = new Process {StartInfo = startProcessInfo};
            process.Start();

            using (var processStandardOutput = process.StandardOutput)
            {
                using (var fileStreamWriter = new StreamWriter(filePathTostreamEnd))
                {
                    fileStreamWriter.WriteLine("---- Beginning of query result ---- ");
                    fileStreamWriter.WriteLine(processStandardOutput.ReadToEnd());
                    fileStreamWriter.WriteLine("---- End of query result ---- ");
                }
            }
        }

        public static void GetAllProcessesInSystem()
        {
            var listOfProcesses = Process.GetProcesses().ToList();

            foreach (var process in listOfProcesses)
            {
                Console.WriteLine("Name: {0}, ID: {1}, Memory: {2}",
                    process.ProcessName,
                    process.Id,
                    process.WorkingSet64);
            }
        }
                

        public static void SearchByName()
        {
            var pathToProcess = "notepad";
            var searchName = "notepad";

            Process.Start(pathToProcess);

            var notepadProcesses = Process.GetProcesses().Where(process => process.ProcessName == searchName).ToList();

            notepadProcesses.ForEach(process =>
                Console.WriteLine("Name: {0}, ID: {1}, Memory: {2}",
                    process.ProcessName,
                    process.Id,
                    process.WorkingSet64));
        }

        public static void SearchAndDestroy()
        {
            var pathToProcess = "notepad";
            var searchName = "notepad";

            Process.Start(pathToProcess);
            Thread.Sleep(1000);

            var notepadProcesses = Process.GetProcesses().Where(process => process.ProcessName == searchName).ToList();

            notepadProcesses.ForEach(process => process.Kill());
        }

        public static void GetThreadsFromProcess()
        {
            var process = Process.GetCurrentProcess();
            var threads = process.Threads;

            Console.WriteLine("Threads for process named {0} with PID: {1}", process.ProcessName, process.Id);
            foreach (ProcessThread thread in threads)
            {
                var threadInfo =
                    $"Thread ID: {thread.Id}\tStart Time: {thread.StartTime}\tPriority: {thread.PriorityLevel}";

                Console.WriteLine(threadInfo);
            }
        }

        public static void GetListOfModules()
        {
            var process = Process.GetCurrentProcess();

            Console.WriteLine("There is a list of modules for: {0}", process.ProcessName);
            foreach (ProcessModule module in process.Modules)
            {
                var moduleInfo = $"Module Name:\t{module.ModuleName}";
                Console.WriteLine(moduleInfo);
            }
        }


        private static void CreateExampleTextFile(string fileName)
        {
            const int maxLines = 20;

            try
            {
                if (File.Exists(fileName)) File.Delete(fileName);

                using (var streamWriter = File.CreateText(fileName))
                {
                    for (var lineIndex = 0; lineIndex < maxLines; lineIndex++)
                        streamWriter.WriteLine("{0}: This is an example line in example text file.", lineIndex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}