public abstract class Produto
{
	public string Nome { get; set; }
	public decimal Preco { get; set; }
	public abstract string ObterCategoria();
	public abstract decimal CalcularFrete();
}

public class Eletronico : Produto
{
	public Eletronico(String nome, double preco)
	{
		super(nome, preco);
	}

	public override string ObterCategoria()
	{
		return "Eletrônicos";
	}

	public override decimal CalcularFrete()
	{
		return Preco * 0.05;
    }
}

public class Roupa : Produto
{
	private string Tamanho { get; set; }

	public Roupa(String nome, double preco, string tamanho)
	{
		super(nome, preco);
		Tamanho = tamanho;
	}
	
	public override string ObterCategoria()
	{
		return "Roupas";
	}

	public override decimal CalcularFrete()
	{
		return 12.50;
    }
}

public class Livro : Produto
{
	private string Author { get; set; }
	private int NumPaginas { get; set; }

	public Livro(String nome, double preco, string author, int numPaginas)
	{
		super(nome, preco);
		Author = author;
		NumPaginas = numPaginas;
	}
	
	public override string ObterCategoria()
	{
		return "Livros";
	}

	public override decimal CalcularFrete()
	{
		if (NumPaginas > 300) return 8;
		else return 5;
    }
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

public class FabricaRoupa : FabricaProduto
{
	public override Produto CriarProduto(string nome, decimal preco, string tamanho)
	{
		return new Roupa { Nome = nome, Preco = preco, Tamanho = tamanho };
	}
}

public class FabricaLivro : FabricaProduto
{
	public override Produto CriarProduto(string nome, decimal preco, string autor, int numPaginas)
	{
		return new Livro { Nome = nome, Preco = preco, Autor = autor, NumPaginas = numPaginas };
	}
}

