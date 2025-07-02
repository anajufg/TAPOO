using System;
using System.Diagnostics;
using System.Numerics;


public struct Personagem
{
	public int Ataque;
	public int Defesa;
	public int ChanceCritico; // 0-100
	public int MultCritico;   // multiplicador de crítico (ex: 200 = 2x)
	public int Vida;
	public bool Vivo;
}


class Program
{
	public static void TestarPerformanceCompleta()
    {
        int[] tamanhosExercito = { 10_000, 50_000, 100_000, 500_000, 1_000_000 };

        Console.WriteLine("=== BENCHMARK DE SISTEMA DE COMBATE ===");
        Console.WriteLine($"SIMD Suportado: {Vector.IsHardwareAccelerated}");
        Console.WriteLine($"Elementos por Vetor: {Vector<int>.Count}");
        Console.WriteLine();

        foreach (int tamanho in tamanhosExercito)
        {
            Console.WriteLine($"Testando exércitos de {tamanho:N0} personagens:");

            var atacantes = SimuladorCombate.GerarExercito(tamanho, "atacante");
            var defensores = SimuladorCombate.GerarExercito(tamanho, "defensor");

            // VERSÃO ORIGINAL
            var swOriginal = Stopwatch.StartNew();
            int danoOriginal = SimuladorCombate.SimularRodadaCombate(atacantes, defensores);
            swOriginal.Stop();

            long tempoOriginal = swOriginal.ElapsedMilliseconds;
            double dpsOriginal = danoOriginal * 1000.0 / Math.Max(1, tempoOriginal);

            // VERSÃO SIMD
            var atacantesSIMD = new ExercitoSIMD(tamanho);
            var defensoresSIMD = new ExercitoSIMD(tamanho);
            atacantesSIMD.ConverterDePersonagens(atacantes);
            defensoresSIMD.ConverterDePersonagens(defensores);

            var swSIMD = Stopwatch.StartNew();
            int danoSIMD = SimuladorCombateSIMD.CalcularDanoVetorizado(atacantesSIMD, defensoresSIMD);
            swSIMD.Stop();

            long tempoSIMD = swSIMD.ElapsedMilliseconds;
            double dpsSIMD = danoSIMD * 1000.0 / Math.Max(1, tempoSIMD);


			Console.WriteLine("=> ORIGINAL");
            Console.WriteLine($"   Dano total: {danoOriginal:N0}");
            Console.WriteLine($"   Tempo: {tempoOriginal} ms");
            Console.WriteLine($"   DPS: {dpsOriginal:N0}\n");

			Console.WriteLine("=> SIMD");
            Console.WriteLine($"   Dano total: {danoSIMD:N0}");
            Console.WriteLine($"   Tempo: {tempoSIMD} ms");
            Console.WriteLine($"   DPS: {dpsSIMD:N0}\n");

            double speedup = (double)tempoOriginal / Math.Max(1, tempoSIMD);
            Console.WriteLine($"Speedup: {speedup:F2}x");
            Console.WriteLine(new string('-', 50));
        }
    }

	static void Main()
	{
		TestarPerformanceCompleta();
	}
}
