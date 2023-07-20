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

|              Method | StrLength |            Mean | Ratio | Allocated | Alloc Ratio |
|-------------------- |---------- |----------------:|------:|----------:|------------:|
|       **StringBuilder** |         **5** |        **44.30 ns** |  **1.00** |     **120 B** |        **1.00** |
| Class.StringCreator |         5 |        43.14 ns |  0.97 |      64 B |        0.53 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |         **7** |        **56.79 ns** |  **1.00** |     **128 B** |        **1.00** |
| Class.StringCreator |         7 |        56.40 ns |  0.99 |      72 B |        0.56 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |        **10** |        **74.07 ns** |  **1.00** |     **144 B** |        **1.00** |
| Class.StringCreator |        10 |        80.66 ns |  1.09 |      80 B |        0.56 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |        **50** |       **331.46 ns** |  **1.00** |     **304 B** |        **1.00** |
| Class.StringCreator |        50 |       346.74 ns |  1.05 |     160 B |        0.53 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |       **100** |       **660.38 ns** |  **1.00** |     **496 B** |        **1.00** |
| Class.StringCreator |       100 |       676.53 ns |  1.02 |     256 B |        0.52 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |      **1000** |     **6,695.47 ns** |  **1.00** |    **4096 B** |        **1.00** |
| Class.StringCreator |      1000 |     6,649.87 ns |  0.99 |    2056 B |        0.50 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |      **2500** |    **16,218.35 ns** |  **1.00** |   **10096 B** |        **1.00** |
| Class.StringCreator |      2500 |    16,578.15 ns |  1.02 |    5056 B |        0.50 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |      **5000** |    **32,402.59 ns** |  **1.00** |   **20096 B** |        **1.00** |
| Class.StringCreator |      5000 |    33,884.77 ns |  1.05 |   10056 B |        0.50 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |     **10000** |    **64,693.92 ns** |  **1.00** |   **40096 B** |        **1.00** |
| Class.StringCreator |     10000 |    66,447.20 ns |  1.03 |   20056 B |        0.50 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |    **100000** |   **748,646.58 ns** |  **1.00** |  **400138 B** |        **1.00** |
| Class.StringCreator |    100000 |   707,204.32 ns |  0.94 |  200077 B |        0.50 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |    **500000** | **3,720,999.90 ns** |  **1.00** | **2000265 B** |        **1.00** |
| Class.StringCreator |    500000 | 3,534,275.66 ns |  0.95 | 1000141 B |        0.50 |
|                     |           |                 |       |           |             |
|       **StringBuilder** |   **1071741** | **7,858,668.65 ns** |  **1.00** | **4287176 B** |        **1.00** |
| Class.StringCreator |   1071741 | 7,359,583.80 ns |  0.94 | 2143628 B |        0.50 |
