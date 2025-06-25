using System.Numerics;

public class SimuladorCombateSIMD
{
    private static Random gerador = new Random(42);

    public static int CalcularDanoVetorizado(ExercitoSIMD atacantes, ExercitoSIMD defensores)
    {
        int danoTotal = 0;
        int tamanhoVetor = Vector<int>.Count;
        int total = atacantes.Ataques.Length;
        // DESAFIO 2: Implementar sistema de crítico vetorizado
        int[] aleatorios = new int[total];
        for (int j = 0; j < total; j++) aleatorios[j] = gerador.Next(0, 100);

        for (int i = 0; i <= total - tamanhoVetor; i += tamanhoVetor)
        {
            var ataque = new Vector<int>(atacantes.Ataques, i);
            var defesa = new Vector<int>(defensores.Defesas, i);
            var chancesCritico = new Vector<int>(atacantes.ChancesCritico, i);
            var multCriticos = new Vector<int>(atacantes.MultCriticos, i);
            var vidasAtacantes = new Vector<int>(atacantes.Vidas, i);
            var vidasDefensores = new Vector<int>(defensores.Vidas, i);

            // DESAFIO 4: Considerar apenas personagens vivos
            var vivosAtacantes = Vector.GreaterThan(vidasAtacantes, Vector<int>.Zero);
            var vivosDefensores = Vector.GreaterThan(vidasDefensores, Vector<int>.Zero);
            var vivosAmbos = Vector.BitwiseAnd(vivosAtacantes, vivosDefensores);

            // DESAFIO 1: Processar dano base (Ataque - Defesa, mínimo 1)
            var danoBase = Vector.Max(Vector<int>.One, Vector.Subtract(ataque, defesa));
            danoBase = Vector.ConditionalSelect(vivosAmbos, danoBase, Vector<int>.Zero);

            // DESAFIO 3: Aplicar multiplicadores de crítico
            var danoCritico = danoBase * multCriticos / 100;

            var aleat = new Vector<int>(aleatorios, i);
            var ehCritico = Vector.LessThan(aleat, chancesCritico);

            danoBase = Vector.ConditionalSelect(ehCritico, danoCritico, danoBase);
            danoTotal += Vector.Dot(danoBase, Vector<int>.One);
        }

        return danoTotal;
    }

}
