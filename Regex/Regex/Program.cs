using System.IO;
using System.Text.RegularExpressions;
using System;

var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()+=_\-{}\[\]:;""'?<>,.]).{7,16}$", RegexOptions.Compiled);

void Atividade1()
{
    var senhaValida = false;

    while (!senhaValida)
    {
        Console.WriteLine("Digite uma senha (ela deve conter 7 a 16 caracteres, pelo menos uma letra maiúscula, uma minúscula, um dígito e um caractere especial):");
        var text = Console.ReadLine() ?? string.Empty;

        if (regex.IsMatch(text))
        {
            Console.WriteLine("Senha válida.\n");
            senhaValida = true;
        }
        else
        {
            Console.WriteLine("Senha inválida.\n");
        }
    }
}

void Atividade2()
{
    string filePath = "prize.json";

    string jsonContent = File.ReadAllText(filePath);

    string pattern = @"""category"":\s*""economics"".*?""firstname"":\s*""([^""]+)""";


    regex = new Regex(pattern);

    MatchCollection matches = regex.Matches(jsonContent);

    Console.WriteLine("Primeiro nome dos ganhadores do premio de economia:");
    foreach (Match match in matches)
        if (match.Groups.Count > 1)
            Console.WriteLine(match.Groups[1].Value);
        else
            Console.WriteLine("Nenhum nome encontrado.");
}

string option;

do
{
    Console.WriteLine("\nEscolha a aitvidade:");
    Console.WriteLine("1 - Atividade 1");
    Console.WriteLine("2 - Atividade 2");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("Opção:");
    option = Console.ReadLine();
    Console.WriteLine("\n");

    switch (option)
    {
        case "1":
            Atividade1();
            break;
        case "2":
            Atividade2();
            break;
        case "0":
            Console.WriteLine("Até mais S2");
            break;
        default:
            break;
    }

} while (option != "0");