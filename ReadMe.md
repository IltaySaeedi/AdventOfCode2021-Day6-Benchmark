# Advent of code 2021 day 6 Benchmark

Build Benchmark project in release mode and run the benchmark

```
Pentium Dual-Core CPU E6300 2.80GHz,
1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.100
Memory: 6 GiB 677 Mhz DDR2
```

|                      Method |            Mean |          Error |         StdDev |          Median |
|---------------------------- |----------------:|---------------:|---------------:|----------------:|
|                 lanternFish | 4,345,062.04 us |  83,392.858 us | 164,609.365 us | 4,326,490.85 us |
|            lanternFishInref | 1,484,001.32 us |  54,274.341 us | 159,177.248 us | 1,503,593.40 us |
| lanternFishInrefResizeArray | 2,285,785.89 us | 129,218.832 us | 381,004.641 us | 2,148,747.90 us |
|   lanternFishBetterApproach |        67.66 us |       2.054 us |       5.827 us |        67.26 us |


```
Pentium Dual-Core CPU E6300 2.80GHz,
1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.100
Memory: 8 GiB 800 Mhz DDR2
```

|                      Method |            Mean |         Error |         StdDev |          Median |
|---------------------------- |----------------:|--------------:|---------------:|----------------:|
|                 lanternFish | 3,351,291.01 us | 44,567.854 us |  41,688.798 us | 3,348,827.20 us |
|            lanternFishInref | 1,198,284.61 us | 38,434.586 us | 113,325.244 us | 1,228,334.55 us |
| lanternFishInrefResizeArray | 1,635,236.72 us | 32,580.831 us |  38,785.162 us | 1,639,185.00 us |
|   lanternFishBetterApproach |        57.44 us |      1.105 us |       1.654 us |        57.19 us |


[Phillip Carter's Solution](https://github.com/cartermp/aoc2021/blob/main/day06/day06_part1.cr) inspired me to write lanternFishBetterApproach
