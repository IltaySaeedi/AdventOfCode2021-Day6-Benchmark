# Advent of code 2021 day 6 Benchmark

Build Benchmark project in release mode and run the benchmark

```
Pentium Dual-Core CPU E6300 2.80GHz,
1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.100
```

|                      Method |            Mean |          Error |         StdDev |          Median |
|---------------------------- |----------------:|---------------:|---------------:|----------------:|
|                 lanternFish | 4,345,062.04 us |  83,392.858 us | 164,609.365 us | 4,326,490.85 us |
|            lanternFishInref | 1,484,001.32 us |  54,274.341 us | 159,177.248 us | 1,503,593.40 us |
| lanternFishInrefResizeArray | 2,285,785.89 us | 129,218.832 us | 381,004.641 us | 2,148,747.90 us |
|   lanternFishBetterApproach |        67.66 us |       2.054 us |       5.827 us |        67.26 us |

I inspired lanternFishBetterApproach from [Phillip Carter's Solution](https://github.com/cartermp/aoc2021/blob/main/day06/day06_part1.cr)