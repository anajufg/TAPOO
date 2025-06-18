using System;
using System.Diagnostics;

var sw = Stopwatch.StartNew();
var initialMemory = GC.GetTotalMemory(true);

ImageProcessorTrivial.ProcessImages();

var finalMemory = GC.GetTotalMemory(true);
sw.Stop();

Console.WriteLine($"Memória inicial: {initialMemory / 1024f / 1024f:F2} MB");
Console.WriteLine($"Memória final: {finalMemory / 1024f / 1024f:F2} MB");
Console.WriteLine($"Diferença de memória: {(finalMemory - initialMemory) / 1024f / 1024f:F2} MB");
Console.WriteLine($"Coleções GC Gen0: {GC.CollectionCount(0)}");
Console.WriteLine($"Coleções GC Gen1: {GC.CollectionCount(1)}");
Console.WriteLine($"Coleções GC Gen2: {GC.CollectionCount(2)}");
