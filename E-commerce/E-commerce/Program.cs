public class SistemaECommerce
{
	public static void Main()
	{
		// 1. Configurar sistema usando Singleton
		var configuracao = GerenciadorConfiguracao.Instancia;

		// 2. Criar produtos usando Factory
		var fabricaEletronicos = new FabricaEletronicos();
		var smartphone = fabricaEletronicos.CriarProduto("iPhone", 999.99m);

		var fabricaRoupas = new FabricaRoupa();
		var camiseta = fabricaRoupas.CriarProduto("Camiseta Branca", 59.90m, "G");

		var fabricaLivros = new FabricaLivro();
		var livro = fabricaLivros.CriarProduto("Clean Code", 89.90m, "Robert C. Martin", 450);

		Console.WriteLine("Produtos criados:");
		Console.WriteLine($"Nome: {smartphone.Nome}");
		Console.WriteLine($"Categoria: {smartphone.ObterCategoria()}");
		Console.WriteLine($"Preço: R$ {smartphone.Preco:F2}\n");

		Console.WriteLine($"Nome: {camiseta.Nome}");
		Console.WriteLine($"Categoria: {camiseta.ObterCategoria()}");
		Console.WriteLine($"Preço: R$ {camiseta.Preco:F2}\n");

		Console.WriteLine($"Nome: {livro.Nome}");
		Console.WriteLine($"Categoria: {livro.ObterCategoria()}");
		Console.WriteLine($"Preço: R$ {livro.Preco:F2}\n");

		// 3. Aplicar decoradores
		var smartphoneComGarantia = new DecoradorGarantia(smartphone, 12);
		var camisetaComEmbalagem = new DecoradorEmbalagem(camiseta);
		var livroComFreteExpresso = new DecoradorFreteExpresso(livro);

		Console.WriteLine("Produtos com decoradores:");
		Console.WriteLine($"Nome: {smartphone.Nome}");
		Console.WriteLine($"Preço com extras: R$ {smartphoneComGarantia.Preco:F2}");
		Console.WriteLine($"Frete calculado: R$ {smartphoneComGarantia.CalcularFrete():F2}\n");

		Console.WriteLine($"Nome: {camiseta.Nome}");
		Console.WriteLine($"Preço com extras: R$ {camisetaComEmbalagem.Preco:F2}");
		Console.WriteLine($"Frete calculado: R$ {camisetaComEmbalagem.CalcularFrete():F2}\n");

		Console.WriteLine($"Nome: {livro.Nome}");
		Console.WriteLine($"Preço com extras: R$ {livroComFreteExpresso.Preco:F2}");
		Console.WriteLine($"Frete calculado: R$ {livroComFreteExpresso.CalcularFrete():F2}\n");

		// 4. Criar pedido e adicionar observadores
		var pedido = new Pedido();
		pedido.Inscrever(new NotificadorEmail());
		pedido.Inscrever(new NotificadorSMS());

		// 5. Processar pagamento usando Strategy
		var contextoPagamento = new ContextoPagamento();
	
		var pagamento = new PagamentoCartaoCredito();
		pagamento.NumCartao = 12345678;
		contextoPagamento.DefinirEstrategiaPagamento(pagamento);

		Console.WriteLine("Processando pagamento...");
		bool pagamentoSucesso = contextoPagamento.ExecutarPagamento(smartphoneComGarantia.Preco);
		if (pagamentoSucesso)
		{
			Console.WriteLine("Pagamento aprovado com sucesso!");
			Console.WriteLine(pagamento.ObterDetalhespagamento() + "\n");

			// 6. Simulação de atualização do pedido (Observer)
			pedido.Status = "Confirmado";
			pedido.Status = "Em transporte";
			pedido.Status = "Entregue";
		}
		else
		{
			Console.WriteLine("Pagamento recusado.");
		}

		var pagamento2 = new PaymentPayPal();
		pagamento2.EmailPayPal = "fulano@gmail.com";
		contextoPagamento.DefinirEstrategiaPagamento(pagamento2);

		Console.WriteLine("Processando pagamento...");
		pagamentoSucesso = contextoPagamento.ExecutarPagamento(camisetaComEmbalagem.Preco);
		if (pagamentoSucesso)
		{
			Console.WriteLine("Pagamento aprovado com sucesso!");
			Console.WriteLine(pagamento2.ObterDetalhespagamento() + "\n");

			pedido.Status = "Confirmado";
			pedido.Status = "Em transporte";
			pedido.Status = "Entregue";
		}
		else
		{
			Console.WriteLine("Pagamento recusado.");
		}

		var pagamento3 = new PagamentoPix();
		pagamento3.ChavePix = 76467433;
		contextoPagamento.DefinirEstrategiaPagamento(pagamento3);

		Console.WriteLine("Processando pagamento...");
		pagamentoSucesso = contextoPagamento.ExecutarPagamento(livroComFreteExpresso.Preco);
		if (pagamentoSucesso)
		{
			Console.WriteLine("Pagamento aprovado com sucesso!");
			Console.WriteLine(pagamento3.ObterDetalhespagamento() + "\n");

			pedido.Status = "Confirmado";
			pedido.Status = "Em transporte";
			pedido.Status = "Entregue";
		}
		else
		{
			Console.WriteLine("Pagamento recusado.");
		}
	}
}
