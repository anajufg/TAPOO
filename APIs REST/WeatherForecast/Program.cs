using System;

ClientHttp client = new ClientHttp();

void ExibirResultadosNoConsole(string simbolo, decimal precoAtual, decimal precoAnterior) 
{
	var corOriginal = Console.ForegroundColor;
	Console.ForegroundColor = precoAtual > precoAnterior ? ConsoleColor.Green : ConsoleColor.Red;
	Console.WriteLine($"{simbolo}: ${precoAtual:N2} {(precoAtual > precoAnterior ? "↑" : "↓")}");
	Console.ForegroundColor = corOriginal;
}

async Task Main()
{
    string? tmp;
    double interval = 0; 
    bool tmpAndIntervalValid = false;

    do
    {
        Console.WriteLine("Escolha uma unidade de tempertura [celsius, kelvin ou fahrenheit]:");
        tmp = Console.ReadLine();

        if (tmp != "celsius" && tmp != "kelvin" && tmp != "fahrenheit")
        {
            Console.WriteLine("Unidade inválida.");
            continue;
        }

        Console.WriteLine("Escolha um intervalo em segundos para efetuar cada nova leitura:");
        string? input = Console.ReadLine();
        if (!double.TryParse(input, out interval) || interval < 0)
        {
            Console.WriteLine("Intervalo inválido.");
            continue;
        }

        tmpAndIntervalValid = true;

    } while (!tmpAndIntervalValid);

    while (true)
    {
        await client.Run();
        await Task.Delay(TimeSpan.FromSeconds(interval));
    }
}

await Main();