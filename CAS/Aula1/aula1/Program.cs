using CAS;

Expressao a = 10;
Expressao b = 0;
Expressao numComplexo = new NumeroComplexo(10, -5);

Expressao soma = a + b;
Expressao divisao = a / b;

Console.WriteLine(divisao.Simplificar());