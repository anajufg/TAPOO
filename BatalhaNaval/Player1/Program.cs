void menu()
    {
        StartServer(9000);

        Console.WriteLine("Vamos jogar Batalha Naval!");
        Console.WriteLine("Escolha o posicionamento de navios:");
        Console.WriteLine("1. Posicionamento Aleatório");
        Console.WriteLine("2. Posicionamento Manual");
        Console.Write("Select an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                break;
            case "2":
                break;
            default:
                Console.WriteLine("Opção inválida. Tente novamente.");
                menu();
                break;
        }
    }