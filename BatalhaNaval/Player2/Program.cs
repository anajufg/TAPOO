const string host = "127.0.0.1";
const int port = 9000;
Player2 player2 = new Player2();

Console.Clear();
Console.WriteLine("Vamos jogar Batalha Naval!");

await player2.ConnectServer(host, port);

while (true)
{
    await player2.Hitting();
}