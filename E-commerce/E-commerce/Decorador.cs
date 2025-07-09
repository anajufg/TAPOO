public abstract class DecoradorProduto : Produto
{
	protected Produto _produto;
   
	public DecoradorProduto(Produto produto)
	{
    	    _produto = produto;
	}
   
	public override string ObterCategoria() => _produto.ObterCategoria();
	public override decimal CalcularFrete() => _produto.CalcularFrete();
}
public class DecoradorGarantia : DecoradorProduto
{
	private int _mesesGarantia;

	public DecoradorGarantia(Produto produto, int mesesGarantia) : base(produto)
	{
		_mesesGarantia = mesesGarantia;
		Preco = produto.Preco + (mesesGarantia * 10); // R$10 por mês
	}

	public override string ObterCategoria() => _produto.ObterCategoria();
   	public override decimal CalcularFrete() => _produto.CalcularFrete();
	
}

public class DecoradorFreteExpresso : DecoradorProduto
{
	public DecoradorFreteExpresso(Produto produto) : base(produto)
	{
		Preco += 20; 
	}

	public override decimal CalcularFrete() => _produto.CalcularFrete() + 15; // acréscimo de R$15 no frete

	public override string ObterCategoria() => base.ObterCategoria() + " + Frete Expresso";

}

public class DecoradorEmbalagem : DecoradorProduto
{
	public DecoradorEmbalagem(Produto produto) : base(produto)
	{
		Preco += 5; 
	}

	public override string ObterCategoria() => base.ObterCategoria() + " + Embalagem Especial";

}