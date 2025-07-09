public abstract class Produto
{
	public string Nome { get; set; }
	public decimal Preco { get; set; }
	public abstract string ObterCategoria();
	public abstract decimal CalcularFrete();
}

public class Eletronico : Produto
{
	public Eletronico(String nome, decimal preco)
	{
		Nome = nome;
		Preco = preco;
	}

	public override string ObterCategoria()
	{
		return "Eletrônicos";
	}

	public override decimal CalcularFrete()
	{
		return Preco * 0.05m;
    }
}

public class Roupa : Produto
{
	public string Tamanho { get; set; }

	public Roupa(String nome, decimal preco, string tamanho)
	{
		Nome = nome;
		Preco = preco;
		Tamanho = tamanho;
	}
	
	public override string ObterCategoria()
	{
		return "Roupas";
	}

	public override decimal CalcularFrete()
	{
		return 12.50m;
    }
}

public class Livro : Produto
{
	public string Author { get; set; }
	public int NumPaginas { get; set; }

	public Livro(String nome, decimal preco, string author, int numPaginas) 
	{
		Nome = nome;
		Preco = preco;
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
		else return 5m;
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
    	   return new Eletronico(nome, preco);
	}
}

public class FabricaRoupa : FabricaProduto
{
	public override Produto CriarProduto(string nome, decimal preco)
	{
		return new Roupa(nome, preco, "M");
	}

	public Produto CriarProduto(string nome, decimal preco, string tamanho)
	{
		return new Roupa(nome, preco, tamanho);
	}
}

public class FabricaLivro : FabricaProduto
{
	public override Produto CriarProduto(string nome, decimal preco)
	{
		return new Livro(nome, preco, "Desconhecido", 100);
	}

	public Produto CriarProduto(string nome, decimal preco, string autor, int numPaginas)
	{
		return new Livro(nome, preco, autor, numPaginas);
	}
}

