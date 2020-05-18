using System;

namespace mandelbrot
{
    class BasicMandelbrot : IMandelBrot
    {
        public string Name { get { return "Basic"; } }
        public void Calculate(byte [] image, Specification s)
        {
            float xdiff = s.FractalLimitX[1] - s.FractalLimitX[0];
            float ydiff = s.FractalLimitY[1] - s.FractalLimitY[0];
            float iter_scale = 1.0f / s.FractalIterations;
            float depth_scale = s.ImageDepth - 1;

            for (int y = 0; y < s.ImageHeight; y++) 
            {
                for (int x = 0; x < s.ImageWidth; x++) 
                {
                    float cr = x * xdiff / s.ImageWidth  + s.FractalLimitX[0];
                    float ci = y * ydiff / s.ImageHeight + s.FractalLimitY[0];
                    float zr = cr;
                    float zi = ci;
                    int k = 0;
                    float mk = 0.0f;
                    while (++k < s.FractalIterations) 
                    {
                        float zr1 = zr * zr - zi * zi + cr;
                        float zi1 = zr * zi + zr * zi + ci;
                        zr = zr1;
                        zi = zi1;
                        mk += 1.0f;
                        if (zr * zr + zi * zi >= 4.0f)
                            break;
                    }
                    mk *= iter_scale;
                    mk = (float)Math.Sqrt(mk);
                    mk *= depth_scale;
                    byte pixel = (byte)mk;
                    image[y * s.ImageWidth * 3 + x * 3 + 0] = pixel;
                    image[y * s.ImageWidth * 3 + x * 3 + 1] = pixel;
                    image[y * s.ImageWidth * 3 + x * 3 + 2] = pixel;
                }
            }
        }
    }
}