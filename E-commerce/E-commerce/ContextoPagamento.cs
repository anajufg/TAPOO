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

public class PagamentoCartaoCredito : IEstrategiaPagamento
{
	private int NumCartao { get; }
	private string NomeTitular { get; }

	public override bool ProcessarPagamento(decimal valor)
	{
		if (valor > 0 && valor < 5000) true;
		else false;
	}

	public override string ObterDetalhespagamento()
	{
		Console.WriteLine("Cartão de Crédito");
		Console.WriteLine(NumCartao % 10000);
	}
}

public class PaymentPayPal : IEstrategiaPagamento
{
	private string EmailPayPal { get; }

	public override bool ProcessarPagamento(decimal valor)
	{
		if (valor > 0 && EmailPayPal != null || EmailPayPal != "") true;
		else false;
	}

	public override string ObterDetalhespagamento()
	{
		Console.WriteLine("PayPal");
		Console.WriteLine($"Email: {EmailPayPal}");
	}
}

public class PagamentoPix : IEstrategiaPagamento
{
	private int ChavePix { get; }

		public override bool ProcessarPagamento(decimal valor)
	{
		if (valor > 0) true;
		else false;
	}

	public override string ObterDetalhespagamento()
	{
		Console.WriteLine("PIX");
		Console.WriteLine($"Chave: {ChavePix}");
	}
}