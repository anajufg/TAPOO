using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ClientHttp
{
    public async Task<(string unidade, double valor)> ObterTemperaturaAsync(string unidade)
    {
        using var client = new HttpClient();

        try
        {
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

            throw new Exception("Resposta da API incompleta ou inválida.");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro ao obter dados: {ex.Message}");
            Console.ResetColor();
            return ("erro", double.NaN);
        }
    }
}
