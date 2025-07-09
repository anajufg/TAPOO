public class SistemaECommerce
{
	public static void Main()
	{
		// 1. Configurar sistema usando Singleton
		var configuracao = GerenciadorConfiguracao.Instancia;

		// 2. Criar produtos usando Factory
		var fabricaEletronicos = new FabricaEletronicos();
		var smartphone = fabricaEletronicos.CriarProduto("iPhone", 999.99m);

		// 3. Aplicar decoradores
		var smartphoneComGarantia = new DecoradorGarantia(smartphone, 12);
		var produtoFinal = new DecoradorFreteExpresso(smartphoneComGarantia);

		// 4. Criar pedido e adicionar observadores
		var pedido = new Pedido();
		pedido.Inscrever(new NotificadorEmail());
		pedido.Inscrever(new NotificadorSMS());

		// 5. Processar pagamento usando Strategy
		var contextoPagamento = new ContextoPagamento();
		contextoPagamento.DefinirEstrategiaPagamento(new PagamentoCartaoCredito());

		Console.WriteLine("Produto final:");
		Console.WriteLine($"Nome: {produtoFinal.Nome}");
		Console.WriteLine($"Categoria: {produtoFinal.ObterCategoria()}");
		Console.WriteLine($"Preço: R$ {produtoFinal.Preco}");
		Console.WriteLine($"Frete: R$ {produtoFinal.CalcularFrete()}\n");
		
		Console.WriteLine("Processando pagamento...");
		bool pagamentoSucesso = contextoPagamento.ExecutarPagamento(produtoFinal.Preco);
		Console.WriteLine(pagamentoSucesso ? "Pagamento aprovado!\n" : "Pagamento recusado.\n");

		// 6. Alterar status do pedido
		if (pagamentoSucesso)
		{
			pedido.Status = "Confirmado";
			pedido.Status = "Enviado";
			pedido.Status = "Entregue";
		}
	}
}
