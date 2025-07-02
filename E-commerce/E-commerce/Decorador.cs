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
   
	// TODO: Implementar métodos
}

// TODO: Implementar DecoradorFreteExpresso e DecoradorEmbalagem Presente
