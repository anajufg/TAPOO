using System;
using System.Diagnostics;

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
	static void Main()
	{
    	const int tamanhoExercito = 500_000; // Meio milhão de personagens!
       
    	Personagem[] atacantes = SimuladorCombate.GerarExercito(tamanhoExercito, "atacante");
    	Personagem[] defensores = SimuladorCombate.GerarExercito(tamanhoExercito, "defensor");
       
    	Console.WriteLine("=== SIMULAÇÃO DE BATALHA ÉPICA ===");
    	Console.WriteLine($"Exércitos: {tamanhoExercito:N0} vs {tamanhoExercito:N0}");
       
    	Stopwatch cronometro = Stopwatch.StartNew();
    	int danoTotalRodada = SimuladorCombate.SimularRodadaCombate(atacantes, defensores);
    	cronometro.Stop();
       
    	Console.WriteLine($"Dano total causado: {danoTotalRodada:N0}");
    	Console.WriteLine($"Tempo sem SIMD: {cronometro.ElapsedMilliseconds}ms");
    	Console.WriteLine($"DPS (danos por segundo): {danoTotalRodada * 1000 / Math.Max(1, cronometro.ElapsedMilliseconds):N0}");
	}
}
