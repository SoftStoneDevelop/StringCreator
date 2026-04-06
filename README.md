<h1 align="center">
  <a>StringCreator</a>
</h1>

<h3 align="center">

  [![Nuget](https://img.shields.io/nuget/v/StringCreator?logo=StringCreator)](https://www.nuget.org/packages/StringCreator/)
  [![Downloads](https://img.shields.io/nuget/dt/StringCreator.svg)](https://www.nuget.org/packages/StringCreator/)
  [![Stars](https://img.shields.io/github/stars/SoftStoneDevelop/StringCreator?color=brightgreen)](https://github.com/SoftStoneDevelop/StringCreator/stargazers)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

</h3>

Assembling a string with a pre-known resulting string length:  uses 50% less memory then StringBuilder.

## Benchmark:

| Method                           | Job            | Runtime        | StrLength | Mean         | Ratio | Allocated | Alloc Ratio |
|--------------------------------- |--------------- |--------------- |---------- |-------------:|------:|----------:|------------:|
| **StringBuilder**                    | **.NET 10.0**      | **.NET 10.0**      | **5**         |     **36.88 ns** |  **1.00** |     **120 B** |        **1.00** |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 5         |    149.72 ns |  4.07 |     152 B |        1.27 |
| Concatenation                    | .NET 10.0      | .NET 10.0      | 5         |    108.69 ns |  2.95 |     248 B |        2.07 |
| StringCreator                    | .NET 10.0      | .NET 10.0      | 5         |     27.45 ns |  0.75 |      64 B |        0.53 |
|                                  |                |                |           |              |       |           |             |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 5         |     40.36 ns |  1.00 |     120 B |        1.00 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 5         |    176.95 ns |  4.40 |     152 B |        1.27 |
| Concatenation                    | NativeAOT 10.0 | NativeAOT 10.0 | 5         |    145.19 ns |  3.61 |     248 B |        2.07 |
| StringCreator                    | NativeAOT 10.0 | NativeAOT 10.0 | 5         |     37.59 ns |  0.93 |      64 B |        0.53 |
|                                  |                |                |           |              |       |           |             |
| **StringBuilder**                    | **.NET 10.0**      | **.NET 10.0**      | **25**        |    **112.02 ns** |  **1.00** |     **200 B** |        **1.00** |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 25        |    531.40 ns |  4.74 |     672 B |        3.36 |
| Concatenation                    | .NET 10.0      | .NET 10.0      | 25        |    623.67 ns |  5.57 |    1848 B |        9.24 |
| StringCreator                    | .NET 10.0      | .NET 10.0      | 25        |    104.48 ns |  0.93 |     104 B |        0.52 |
|                                  |                |                |           |              |       |           |             |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 25        |    144.13 ns |  1.00 |     200 B |        1.00 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 25        |    616.37 ns |  4.28 |     672 B |        3.36 |
| Concatenation                    | NativeAOT 10.0 | NativeAOT 10.0 | 25        |    817.48 ns |  5.67 |    1848 B |        9.24 |
| StringCreator                    | NativeAOT 10.0 | NativeAOT 10.0 | 25        |    149.63 ns |  1.04 |     104 B |        0.52 |
|                                  |                |                |           |              |       |           |             |
| **StringBuilder**                    | **.NET 10.0**      | **.NET 10.0**      | **100**       |    **406.36 ns** |  **1.00** |     **496 B** |        **1.00** |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100       |  1,949.56 ns |  4.80 |    2624 B |        5.29 |
| Concatenation                    | .NET 10.0      | .NET 10.0      | 100       |  2,744.22 ns |  6.75 |   14976 B |       30.19 |
| StringCreator                    | .NET 10.0      | .NET 10.0      | 100       |    392.47 ns |  0.97 |     256 B |        0.52 |
|                                  |                |                |           |              |       |           |             |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100       |    530.65 ns |  1.00 |     496 B |        1.00 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100       |  2,378.89 ns |  4.49 |    2624 B |        5.29 |
| Concatenation                    | NativeAOT 10.0 | NativeAOT 10.0 | 100       |  3,869.99 ns |  7.30 |   14976 B |       30.19 |
| StringCreator                    | NativeAOT 10.0 | NativeAOT 10.0 | 100       |    562.36 ns |  1.06 |     256 B |        0.52 |
|                                  |                |                |           |              |       |           |             |
| **StringBuilder**                    | **.NET 10.0**      | **.NET 10.0**      | **1000**      |  **3,891.79 ns** |  **1.00** |    **4096 B** |        **1.00** |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1000      | 19,318.02 ns |  4.96 |   26024 B |        6.35 |
| Concatenation                    | .NET 10.0      | .NET 10.0      | 1000      | 63,794.51 ns | 16.39 | 1049976 B |      256.34 |
| StringCreator                    | .NET 10.0      | .NET 10.0      | 1000      |  3,693.73 ns |  0.95 |    2056 B |        0.50 |
|                                  |                |                |           |              |       |           |             |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |  5,190.07 ns |  1.00 |    4096 B |        1.00 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1000      | 23,148.44 ns |  4.47 |   26024 B |        6.35 |
| Concatenation                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      | 72,140.25 ns | 13.95 | 1049976 B |      256.34 |
| StringCreator                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |  5,201.16 ns |  1.01 |    2056 B |        0.50 |


Usage:

```C#

            var stringCreator = new StringCreator.Class.StringCreator(50);
            for (int i = 0; i < 50; i++)
            {
                stringCreator.Append($"{i % 10}");
            }

            var result = stringCreator.ToString();

```
