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
                char cell = board[r, c];
                Console.Write(!showShips && cell == '*' ? "~ " : $"{cell} ");
            }
            Console.WriteLine();
        }
    }

    public void PlaceShipsRandomly(int n)
    {
        var rnd = new Random();
        int placed = 0;
        while (placed < n)
        {
            int r = rnd.Next(10), c = rnd.Next(10);
            if (board[r, c] == '~')
            {
                board[r, c] = '*';
                placed++;
            }
        }
    }

    public void PlaceShipsManually(int n)
    {
        Console.WriteLine("Posicione os navios (ex: A1, B2, ...):");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Posição navio {i + 1}: ");
            string input = Console.ReadLine();
            int r = input[0] - 'A';
            int c = int.Parse(input[1].ToString());
            if (!IsShip(r, c))
            {
                board[r, c] = '*';
            }
            else if (IsShip(r, c))
            {
                Console.WriteLine("Posição já ocupada. Tente novamente.");
                i--;
            }
            else
            {
                Console.WriteLine("Posição inválida. Tente novamente.");
                i--;
            }
        }
    }

    public bool IsShip(int r, int c)
    {
        return board[r, c] == '*';
    }

    public bool MarkHit(int r, int c)
    {
        if (IsShip(r, c))
        {
            board[r, c] = 'X';
            return true;
        }
        else
        {
            return MarkMiss(r, c);
        }
    }

    public bool MarkMiss(int r, int c)
    {
        if (board[r, c] == '~')
        {
            board[r, c] = 'O';
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