using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForEach.Demo
{
    public class ImageProcessor
    {
        public async Task ProcessImagesAsync(string file, string destination)
        {
            string fileName = Path.GetFileName(file);
            var bitmap = new Bitmap(file);

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            bitmap.Save(Path.Combine(destination, fileName));

            Console.WriteLine($"Processando {fileName} no thread {Thread.CurrentThread.ManagedThreadId}");
            await Task.CompletedTask;
        }

    }
}
