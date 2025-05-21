using System.Threading.Tasks;
﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Player1
{
    const int port;
    var player2;
    using var stream;

    public async Task StartServer()
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
        await stream.Write(data, 0, data.Length,);
    }

    public async Task<string> Receive()
    {
        var buffer = new byte[32];
        int len = await stream.Read(buffer, 0, buffer.Length);
        return Encoding.ASCII.GetString(buffer, 0, len);
    }
}