using CAS;

Expressao a = 10;
Simbolo b = new Simbolo("b");
Simbolo c = new Simbolo("c");
Expressao numComplexo = new NumeroComplexo(10, -5);

Expressao soma = a + b;
Expressao divisao = a / b;

Console.WriteLine(soma.Simplificar());

soma = soma.Substituir(b, 5);

Expressao bt = b.Substituir(c, 3);

Console.WriteLine(bt.ToString());
