using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ClientHttp
{
	public async Task Run()
	{
		using var client = new HttpClient();
		var response = await client.GetStringAsync("http://localhost:5178/temperatura/celsius");

		var result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

		if (result != null) Console.WriteLine($"Unidade: {result["unidade"]} | Valor: {result["valor"]} | Horário: {DateTime.Now.ToString("HH:mm:ss")}");

	}
}
