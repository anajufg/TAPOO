public class Board
{
    public int size;
    public char[] board;

    public Board(int size)
    {
        this.size = size;
        this.board = new char[size * size];
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = '~';
        }
    }

    public void Print(bool showShips)
    {
        Console.Write("   ");
        for (int c = 0; c < 10; c++) Console.Write($"{c} ");
        Console.WriteLine();
        for (int r = 0; r < 10; r++)
        {
            Console.Write($"{(char)('A' + r)}  ");
            for (int c = 0; c < 10; c++)
            {
                char cell = board[r * size + c];
                Console.Write(!showShips && cell == '*' ? "~ " : $"{cell} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("\n");
    }

    public void PlaceShipsRandomly(int nShips)
    {
        var rnd = new Random();
        int placed = 0;
        while (placed < nShips)
        {
            int r = rnd.Next(10), c = rnd.Next(10);
            if (board[r * size + c] == '~')
            {
                board[r * size + c] = '*';
                placed++;
            }
        }

        Print(true);
    }

    public void PlaceShipsManually(int nShips)
    {
        Console.WriteLine("Posicione os navios (ex: A1, B2, ...):");
        for (int i = 0; i < nShips; i++)
        {
            Console.Write($"Posição navio {i + 1}: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input.Length < 2)
            {
                Console.WriteLine("Posição inválida. Tente novamente.");
                i--;
                continue;
            }

            int r = input[0] - 'A';
            int c;
            if (!int.TryParse(input[1].ToString(), out c) || r < 0 || r >= size || c < 0 || c >= size)
            {
                Console.WriteLine("Posição inválida. Tente novamente.");
                i--;
                continue;
            }
            if (!IsShip(r, c))
            {
                board[r * size + c] = '*';
            }
            else
            {
                Console.WriteLine("Posição já ocupada. Tente novamente.");
                i--;
            }
        }

        Print(true);
    }

    public bool IsShip(int r, int c)
    {
        return board[r * size + c] == '*';
    }

    public bool MarkHit(int r, int c)
    {
        if (IsShip(r, c))
        {
            board[r * size + c] = 'X';
            return true;
        }
        else
        {
            return MarkMiss(r, c);
        }
    }

    public bool MarkMiss(int r, int c)
    {
        if (board[r * size + c] == '~')
        {
            board[r * size + c] = 'O';
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AreAllShipsSunk()
    {
        foreach (var cell in board)
        {
            if (cell == '*') return false;
        }
        return true;
    }
}