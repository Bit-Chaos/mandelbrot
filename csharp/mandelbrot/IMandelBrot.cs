using System;

namespace mandelbrot
{
    public class Specification
    {
        /// <summary>
        /// Image width
        /// </summary>
        public int ImageWidth = 1440*2;
        /// <summary>
        /// Image height
        /// </summary>
        public int ImageHeight = 1080*2;
        /// <summary>
        /// Image depth (colors)
        /// </summary>
        public int ImageDepth = 256;
        /// <summary>
        /// Fractal specification x-limit
        /// </summary>
        public float [] FractalLimitX = { -2.5f, 1.5f };
        /// <summary>
        /// Fractal specification y-limit
        /// </summary>
        public float [] FractalLimitY = { -1.5f, 1.5f };
        /// <summary>
        /// Iterations for fractal
        /// </summary>
        public int FractalIterations = 256;
    }

    public interface IMandelBrot
    {
        string Name { get; }
        void Calculate(byte [] image, Specification s);
    }
}