const int port = 9000;
Player1 player1 = new Player1();

await player1.StartServer(port);

Console.Clear();
Console.WriteLine("Vamos jogar Batalha Naval!");
Console.WriteLine("Digite o número de navios:");
int nShips = int.Parse(Console.ReadLine());
int choice;

do
{
    Console.WriteLine("Escolha o posicionamento de navios:");
    Console.WriteLine("1. Posicionamento Aleatório");
    Console.WriteLine("2. Posicionamento Manual");
    choice = int.Parse(Console.ReadLine());

    switch (choice)
    {
        case 1:
            player1.board.PlaceShipsRandomly(nShips);
            break;
        case 2:
            player1.board.PlaceShipsManually(nShips);
            break;
        case 0:
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente");
            return;
    }
} while (choice != 1 && choice != 0);

await player1.Run();

