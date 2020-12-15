using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ParallelForEach.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ThreadPool.SetMinThreads(50, 50);
            string[] files = Directory.GetFiles(@"C:\Users\WellingtonKarl\Pictures\Imagens", "*.jpg");
            string newDir = @"C:\Users\WellingtonKarl\Desktop\Parallel2";
            Directory.CreateDirectory(newDir);

            //var initial = DateTime.Now;
            var stopwatch = new Stopwatch();
            List<Task> tasks = new List<Task>();
            ImageProcessor processor = new ImageProcessor();

            //teste com foreach parallel
            stopwatch.Start();
            Parallel.ForEach(files, (currentFile) =>
            {
                tasks.Add(Task.Run(async () => await processor.ProcessImagesAsync(currentFile, newDir)));
            });
            Task.WaitAll(tasks.ToArray());
            stopwatch.Stop();
            Console.WriteLine("Fim do processo paralelo!");
            Console.WriteLine($"Tempo de processamento paralelo: {stopwatch.ElapsedMilliseconds} ms");
            Console.ReadKey();

            //Teste com Foreach normal
            stopwatch.Start();
            foreach (var currentFile in files)
            {
               await processor.ProcessImagesAsync(currentFile, newDir);
            }
            stopwatch.Stop();
            Console.WriteLine("Fim do processo foreach normal");
            Console.WriteLine($"Tempo de processamento normal: {stopwatch.ElapsedMilliseconds} ms");
            Console.ReadKey();

            Console.WriteLine("Processamento finalizado com sucesso, precione qualquer tecla para sair!");
            Console.ReadKey();
        }
    }
}
