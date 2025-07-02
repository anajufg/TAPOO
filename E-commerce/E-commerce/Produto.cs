public abstract class Produto
{
	public string Nome { get; set; }
	public decimal Preco { get; set; }
	public abstract string ObterCategoria();
	public abstract decimal CalcularFrete();
}

public abstract class FabricaProduto
{
	public abstract Produto CriarProduto(string nome, decimal preco);
}

public class FabricaEletronicos : FabricaProduto
{
	public override Produto CriarProduto(string nome, decimal preco)
	{
    	    return new Eletronico { Nome = nome, Preco = preco };
	}
}

// TODO: Implementar FabricaRoupa e FabricaLivro
