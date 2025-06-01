using System.Net;
using System.Net.Sockets;
using System.Text;


public class Player1
{
    private TcpClient? player2;
    private NetworkStream? stream;
    public Board board = new Board(10);


    public async Task StartServer(int port)
    {
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Aguardando Player2 na porta {port}...");
        player2 = await listener.AcceptTcpClientAsync();
        stream = player2.GetStream();
        Console.WriteLine("Player2 conectado!");
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

    public async Task Run()
    {
        while (true)
        {
            string coordinate = await Receive();
            if (coordinate.Length < 2) continue;
            Console.WriteLine($"Coordenada recebida: {coordinate}");


            int r = coordinate[0] - 'A';
            int c = int.Parse(coordinate[1].ToString());

            if (r < 0 || r >= board.size || c < 0 || c >= board.size)
            {
                await Send("MISS");
                continue;
            }

            bool hit = board.MarkHit(r, c);
            board.Print(false);

            if (hit)
            {
                if (board.AreAllShipsSunk())
                {
                    await Send("WIN");
                    break;
                }
                else
                {
                    await Send("HIT");
                }
            }
            else
            {
                await Send("MISS");
            }
        }
    }
}