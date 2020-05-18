# A really simple comparison of computing performance between C and C#

This is not meant to start any religious war between the various programming languages but merely a short test for finding out the current status-quo of the AVX2 implementation for the dotnet core framework. Also it is not meant as a scientifical report, neither does it try to implement the most efficient algorithm of the mandelbrot set.

So let's jump straight into AVX using good old C and C#. 
The C-part utilises the algorithm from [Mandelbrot Set with SIMD Intrinsics](http://nullprogram.com/blog/2015/07/10/) and the C# part was ported more or less straight from C to C#. 

Several tests where run inside Docker-desktop on a Dell-laptop which did show the following results. The specs should not matter so much since I was just interested in the relative performance between a native C-application and a managed C# application. For C# the .NET core SDK 5.0 preview-3 was used. C used the gcc 8.x.

## Simple implementation

The first test was done using no SIMD instructions at all, i.e. plain implementation (C: mandel.c/mandel_basic C#: Basic.cs). OpenMP was disabled in order to be able to compare the results from the algorithm in both languages. Serveral runs where performed and the smallest number was used, so that swapping effects/other background applications etc. where stripped off, so that the best achievable result could be used (assuming best-case). 

| Image size | Language | Time (ms) | Relative |
|------------|----------|-----------|----------|
| 1440x1080  | C        | 203       | 100 %    |
| 1440x1080  | C#       | 208       | 102.5 %  |
| 2880x2060  | C        | 813       | 100 %    |
| 2880x2060  | C#       | 831       | 102.2 %  |

## AVX implementation

C# is implemented inside AVX.cs and C used the algorithm from mandel_avx.c.

| Image size | Language | Time (ms) | Relative |
|------------|----------|-----------|----------|
| 1440x1080  | C        | 32        | 100 %    |
| 1440x1080  | C#       | 33        | 103.1 %  |
| 2880x2060  | C        | 127       | 100 %    |
| 2880x2060  | C#       | 134       | 105.5 %  |
| 5750x4320  | C        | 805       | 100 %    |
| 5760x4320  | C#       | 823       | 102.2 %  |


## Conclusion

This really simplistic test shows that the C# implementation is between 2%-5% slower than our good old C for calculation intensive tasks ... well, actually not bad. Even 10% would open the option for me going over to C# instead of using C/C++ for computational tasks. Memory management might be another topic. Especially the behaviour of the garbage collector at a project running over a long time might be an issue to be addressed ... but there's a ton of documents out there which already addresses all these issues and I'm not going to repeat all that stuff here.
I see the benefit using the .NET runtime in having a high-level language which opens up for low-level on demand. So one has the safety belts around for all the general stuff and when it get's critical one can shoot himself into the knee. Using C/C++ you can shoot yourself all the time (and yes/no/hmm ... I like C++/C as well and don't want to start bashing that language). So it's not unexpected that C# is slower than C since it has some wrapper code around the primitives, but it is not bad at all either.


## External Links
* [Hardware Intrinsics in .NET Core](https://devblogs.microsoft.com/dotnet/hardware-intrinsics-in-net-core/)
* [Mandelbrot Set with SIMD Intrinsics](http://nullprogram.com/blog/2015/07/10/)
