using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace mandelbrot
{
    class Program
    {
        static void WritePPM(Specification spec, byte[] bytes, string filename)
        {
            using(var fs = new System.IO.FileStream(filename, FileMode.Create))
            using(var writer = new System.IO.BinaryWriter(fs))
            {
                string header = String.Format("P6\n{0} {1}\n{2}\n", spec.ImageWidth, spec.ImageHeight, spec.ImageDepth-1);
                writer.Write(Encoding.ASCII.GetBytes(header));
                writer.Write(bytes, 0, spec.ImageWidth*spec.ImageHeight*3);
            }
        }
        static void RunTest(IMandelBrot algorithm, byte [] image, Specification specification, String filename)
        {
            // warm up JIT
            algorithm.Calculate(image, specification);
            var sw = new Stopwatch();
            const int count = 20;
            sw.Start();
            for(int i=0; i<count; i++)
            {
                algorithm.Calculate(image, specification);
            }
            sw.Stop();
            // write result to file
            WritePPM(specification, image, filename);
            //double duration = sw.
            Console.WriteLine("C# {0}: {1} ms/run", algorithm.Name, sw.ElapsedMilliseconds / count);

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Calculating w/o optimization");
            var spec = new Specification();
            byte [] image = new byte[3*spec.ImageWidth*spec.ImageHeight];
            RunTest(new BasicMandelbrot(), image, spec, "output_basic.ppm");
            RunTest(new AVXMandelbrot(), image, spec, "output_avx.ppm");
            
            Console.WriteLine("test run finished");
        }
    }
}
