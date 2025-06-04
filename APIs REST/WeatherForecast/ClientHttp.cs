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

		public async Task<(string unidade, double valor)> Run(string unidade)
		{
			using var client = new HttpClient();
			var response = await client.GetStringAsync($"http://localhost:5178/temperatura/{unidade}");

			var result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

			if (result != null &&
				result.TryGetValue("unidade", out var unidadeObj) &&
				result.TryGetValue("valor", out var valorObj) &&
				unidadeObj is JsonElement unidadeJson &&
				valorObj is JsonElement valorJson &&
				valorJson.TryGetDouble(out double valor))
			{
				return (unidadeJson.GetString() ?? "desconhecida", valor);
			}

			throw new Exception("Erro ao obter dados da API.");
		}


}
