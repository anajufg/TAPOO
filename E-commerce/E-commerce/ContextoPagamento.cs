public interface IEstrategiaPagamento
{
	bool ProcessarPagamento(decimal valor);
	string ObterDetalhespagamento();
}

public class ContextoPagamento
{
	public IEstrategiaPagamento _estrategiaPagamento;
   
	public void DefinirEstrategiaPagamento(IEstrategiaPagamento estrategia)
	{
    	    _estrategiaPagamento = estrategia;
	}
   
	public bool ExecutarPagamento(decimal valor)
	{
    	    return _estrategiaPagamento?.ProcessarPagamento(valor) ?? false;
	}
}

public class PagamentoCartaoCredito : IEstrategiaPagamento
{
	public int NumCartao { set; get; }
	public string NomeTitular { set; get; }

	public bool ProcessarPagamento(decimal valor)
	{
		return valor > 0 && valor < 5000;
	}

	public string ObterDetalhespagamento()
	{
		return $"Cartão de Crédito\n****{NumCartao % 10000}";
	}
}

public class PaymentPayPal : IEstrategiaPagamento
{
	public string EmailPayPal { set; get; }

	public bool ProcessarPagamento(decimal valor)
	{
		return (valor > 0 && EmailPayPal != null) || EmailPayPal != "";
	}

	public string ObterDetalhespagamento()
	{
		return $"PaymentPayPal\nEmail: {EmailPayPal}";
	}
}

public class PagamentoPix : IEstrategiaPagamento
{
	public int ChavePix { set;  get; }

	public bool ProcessarPagamento(decimal valor)
	{
		return valor > 0;
	}

	public string ObterDetalhespagamento()
	{
		return $"PIX\nChave: {ChavePix}";
	}
}