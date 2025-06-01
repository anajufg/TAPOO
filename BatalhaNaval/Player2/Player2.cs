using System.Net.Sockets;
using System.Text;

public class Player2
{
    private NetworkStream? stream;

    public async Task ConnectServer(string host, int port)
    {
        var client = new TcpClient();
        await client.ConnectAsync(host, port);
        stream = client.GetStream();
    }

    public async Task Send(string msg)
    {
        var data = Encoding.ASCII.GetBytes(msg);
        await stream.WriteAsync(data, 0, data.Length);
    }

    public async Task<string> Receive()
    {
        var buffer = new byte[32];
        int len = await stream.ReadAsync(buffer, 0, buffer.Length);
        return Encoding.ASCII.GetString(buffer, 0, len);
    }

    public async Task Hitting()
    {
        Console.WriteLine("Digite a coordenada do ataque (ex: A1, B2, ...):");
        string coordinate = Console.ReadLine();

        await Send(coordinate);
        string response = await Receive();
        Console.WriteLine(response);
        
        Console.WriteLine("\n");

    }
}