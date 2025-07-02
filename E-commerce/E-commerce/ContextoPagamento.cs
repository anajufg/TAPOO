public interface IEstrategiaPagamento
{
	bool ProcessarPagamento(decimal valor);
	string ObterDetalhespagamento();
}

public class ContextoPagamento
{
	private IEstrategiaPagamento _estrategiaPagamento;
   
	public void DefinirEstrategiaPagamento(IEstrategiaPagamento estrategia)
	{
    	    _estrategiaPagamento = estrategia;
	}
   
	public bool ExecutarPagamento(decimal valor)
	{
    	    return _estrategiaPagamento?.ProcessarPagamento(valor) ?? false;
	}
}

// TODO: Implementar PagamentoCartaoCredito, PaymentPayPal, PagamentoPix