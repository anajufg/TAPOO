using System;

ClientHttp client = new ClientHttp();
CancellationTokenSource cts = new CancellationTokenSource();

void ExibirTemperatura(string unidade, double valorAtual, double? valorAnterior)
{
    var seta = "-";
    var corOriginal = Console.ForegroundColor;
    if (valorAtual > valorAnterior)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        seta = "↑";
    }
    else if (valorAnterior > valorAtual)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        seta = "↓";
    }
    Console.WriteLine($"{seta} Unidade: {unidade} | Valor: {valorAtual:N2} | Horário: {DateTime.Now.ToString("HH:mm:ss")}");
    Console.ForegroundColor = corOriginal;
}

Console.CancelKeyPress += (sender, e) =>
{
    Console.WriteLine("\nEncerrando monitoramento de temperatura...");
    e.Cancel = true; 
    cts.Cancel();    
};

async Task Main()
{
    string? unidade;
    double intervalo = 0;
    double? valorAnterior = null;
    bool dadosValidos = false;

    Console.Clear();

    do
    {
        Console.WriteLine("Escolha uma unidade de tempertura [celsius, kelvin ou fahrenheit]:");
        unidade = Console.ReadLine()?.ToLower();

        if (unidade != "celsius" && unidade != "kelvin" && unidade != "fahrenheit")
        {
            Console.WriteLine("Unidade inválida.");
            continue;
        }

        Console.WriteLine("Escolha um intervalo (em segundos) entre as leituras:");
        if (!double.TryParse(Console.ReadLine(), out intervalo) || intervalo <= 0)
        {
            Console.WriteLine("Intervalo inválido.");
            continue;
        }

        dadosValidos = true;

    } while (!dadosValidos);

    while (!cts.Token.IsCancellationRequested)
    {
        var (unidadeObtida, valor) = await client.ObterTemperaturaAsync(unidade);

        if (valor != null)
        {
            ExibirTemperatura(unidadeObtida, valor, valorAnterior);
            valorAnterior = valor;
        }

        try
        {
            await Task.Delay(TimeSpan.FromSeconds(intervalo), cts.Token);
        }
        catch (TaskCanceledException)
        {
            break;
        }
    }
}

await Main();