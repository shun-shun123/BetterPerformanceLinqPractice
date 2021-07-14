using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace ZNext
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddExporter(MarkdownExporter.GitHub);
            AddDiagnoser(MemoryDiagnoser.Default);

            AddJob(Job.Default);
        }
    }


    [Config(typeof(BenchmarkConfig))]
    public class ZFirstOrDefaultBenchmark
    {
        private const int ARRAY_SIZE = 100;

        private const int TRY_COUNT = 100;

        private int[] _sampleArray;

        private List<int> _sampleList;

        [GlobalSetup]
        public void Setup()
        {
            var rand = new Random();
            _sampleArray = Enumerable.Range(0, ARRAY_SIZE).Select(_ => rand.Next(1000)).ToArray();
            _sampleList = _sampleArray.ToList();
        }

        [Benchmark]
        public void Linq()
        {
            var _ = _sampleArray.FirstOrDefault(IsOver);
        }

        [Benchmark]
        public void ArrayZFirstOrDefault()
        {
            unsafe
            {
                var _ = _sampleArray.ZFirstOrDefault(&IsOver);
            }
        }

        [Benchmark]
        public void ListZFirstOrDefault()
        {
            unsafe
            {
                var _ = _sampleList.ZFirstOrDefault(&IsOver);
            }
        }

        private static bool IsOver(int number)
        {
            return number > 500;
        }
    }
}