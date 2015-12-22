# Benchmarks

Source code for article about HTML parsers in .NET.

## Results

Short results from benchmarks

### AHrefBenchmark

The benchmark is about one of the most popular topic - extract all anchor tags from html page

[Read the benchmark's code](src/Benchmarks.HtmlParsers/Benchmarks/AHrefBenchmark.cs)

> **BenchmarkDotNet**=v0.8.0.0

> **OS**=Microsoft Windows NT 6.1.7601 Service Pack 1

> **Processor**=Intel(R) Core(TM) i7-4770 CPU @ 3.40GHz, ProcessorCount=8

> **HostCLR**=MS.NET 4.0.30319.34209, Arch=32-bit 

> **Type**=AHrefBenchmark  Mode=Throughput  Platform=HostPlatform  Jit=HostJit  .NET=HostFramework  toolchain=Classic  Runtime=Clr  Warmup=5  Target=10

          Method |    AvrTime |    StdDev |   op/s |
---------------- |----------- |---------- |------- |
      AngleSharp |  8.7233 ms | 0.4735 ms | 114.94 |
         CsQuery | 12.7652 ms | 0.2296 ms |  78.36 |
         Fizzler |  5.9388 ms | 0.1080 ms | 168.44 |
 HtmlAgilityPack |  5.4742 ms | 0.1205 ms | 182.76 |
           Regex |  3.2897 ms | 0.1240 ms | 304.37 |

### TableBenchmark

The benchmark is was created as result of one the default parser's way of usage - parsing html data in tables

[Read the benchmark's code](src/Benchmarks.HtmlParsers/Benchmarks/TableBenchmark.cs)

> **BenchmarkDotNet**=v0.8.0.0

> **OS**=Microsoft Windows NT 6.1.7601 Service Pack 1

> **Processor**=Intel(R) Core(TM) i7-4770 CPU @ 3.40GHz, ProcessorCount=8

> **HostCLR**=MS.NET 4.0.30319.34209, Arch=32-bit 

> **Type**=TableBenchmark  Mode=Throughput  Platform=HostPlatform  Jit=HostJit  .NET=HostFramework  toolchain=Classic  Runtime=Clr  Warmup=5  Target=10  


          Method |    AvrTime |    StdDev |  op/s |
---------------- |----------- |---------- |------ |
      AngleSharp | 27.4181 ms | 1.1380 ms | 36.53 |
         CsQuery | 42.2388 ms | 0.7857 ms | 23.68 |
         Fizzler | 21.7716 ms | 0.6842 ms | 45.97 |
 HtmlAgilityPack | 20.6314 ms | 0.3786 ms | 48.49 |
           Regex | 42.2942 ms | 0.1382 ms | 23.64 |
