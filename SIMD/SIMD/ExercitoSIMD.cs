public class ExercitoSIMD
{
	public int[] Ataques;
	public int[] Defesas;
	public int[] ChancesCritico;
	public int[] MultCriticos;
	public int[] Vidas;
	public bool[] Vivos;
   
	public ExercitoSIMD(int tamanho)
	{
    	Ataques = new int[tamanho];
    	Defesas = new int[tamanho];
    	ChancesCritico = new int[tamanho];
    	MultCriticos = new int[tamanho];
    	Vidas = new int[tamanho];
    	Vivos = new bool[tamanho];
	}
   
	public void ConverterDePersonagens(Personagem[] personagens)
	{
    	// Implementar conversão
	}
}
