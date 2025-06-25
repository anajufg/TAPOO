// using System.Numerics;

// public class SimuladorCombate
// {
//     private static Random gerador = new Random(42);

//     public static int CalcularDano(ExercitoSIMD atacantes, ExercitoSIMD defensores, int index)
//     {
//         if (!atacantes.Vivos[index] || !defensores.Vivos[index])
//             return 0;

//         // Dano base = Ataques - Defesas (mínimo 1)
//         int danoBase = Math.Max(1, atacantes.Ataques[index] - defensores.Defesas[index]);

//         // Verificar crítico
//         bool ehCritico = gerador.Next(0, 100) < atacantes.ChancesCritico[index];

//         if (ehCritico)
//         {
//             danoBase = (danoBase * atacantes.MultCriticos[index]) / 100;
//         }

//         return danoBase;
//     }

//     public static int SimularRodadaCombate(ExercitoSIMD atacantes, ExercitoSIMD defensores)
//     {
//         int danoTotal = 0;

//         for (int i = 0; i < atacantes.Vivos.Length && i < defensores.Vivos.Length; i++)
//         {
//             danoTotal += CalcularDano(atacantes, defensores, i);
//         }

//         return danoTotal;
//     }

//     public static ExercitoSIMD GerarExercito(int tamanho, string tipo)
//     {
//         ExercitoSIMD exercito = new ExercitoSIMD(tamanho);

//         for (int i = 0; i < tamanho; i++)
//         {
//             if (tipo == "atacante")
//             {

//                 exercito.Ataques[i] = gerador.Next(80, 120);     // 80-119 ataques
//                 exercito.Defesas[i] = gerador.Next(20, 40);     // 20-39 defesas 
//                 exercito.ChancesCritico[i] = gerador.Next(15, 25); // 15-24% crítico
//                 exercito.MultCriticos[i] = gerador.Next(180, 220); // 1.8x-2.2x crítico
//                 exercito.Vidas[i] = gerador.Next(100, 150);
//                 exercito.Vivos[i] = true;

//             }
//             else // defensor
//             {

//                 exercito.Ataques[i] = gerador.Next(60, 80);   	// menos ataques
//                 exercito.Defesas[i] = gerador.Next(40, 70);   	// mais defesas
//                 exercito.ChancesCritico[i] = gerador.Next(10, 20);
//                 exercito.MultCriticos[i] = gerador.Next(150, 200);
//                 exercito.Vidas[i] = gerador.Next(120, 180);   	// mais vidas
//                 exercito.Vivos[i] = true;

//             }
//         }

//         return exercito;
//     }
// }