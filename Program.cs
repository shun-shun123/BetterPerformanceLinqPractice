using BenchmarkDotNet.Running;

namespace ZNext
{
    class Program
    {
        static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(ZFirstOrDefaultBenchmark)
            });
            switcher.Run(args);
        }
    }
}
