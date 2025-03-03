namespace CAS;

public abstract class Expressao 
{
    public abstract override string ToString();
    public abstract Expressao Derivar(Simbolo x);
    public abstract Expressao Simplificar();
    public abstract Expressao Substituir(Simbolo substituido, Expressao substituto);

    public static Expressao operator +(Expressao a, Expressao b) => new Soma(a, b);
    public static Expressao operator -(Expressao a, Expressao b) => new Subtracao(a, b);
    public static Expressao operator *(Expressao a, Expressao b) => new Multiplicao(a, b);
    public static Expressao operator /(Expressao a, Expressao b) => new Divisao(a, b);

    public static implicit operator Expressao(int v) => new Numero(v);
    public static implicit operator Expressao(string s) => new Simbolo(s);

}

public class Numero : Expressao 
{

    int valor;

    public Numero(int v) => this.valor = v;

    public int getValor() => this.valor;

    public override string ToString() => valor.ToString();

    public override Expressao Derivar(Simbolo x) => new Numero(0);

    public override Expressao Simplificar() => this;

    public override Expressao Substituir(Simbolo substituido, Expressao substituto) => this;

}

public class Simbolo : Expressao 
{

    string simbolo;

    public Simbolo(string s) => this.simbolo = s;

    public string getSimbolo() => this.simbolo;

    public override string ToString() => simbolo;

    public override Expressao Derivar(Simbolo x) =>
        x.simbolo == simbolo
            ? new Numero(1)
            : new Numero(0);

    public override Expressao Simplificar() => this;

    public override Expressao Substituir(Simbolo substituido, Expressao substituto) => 
        substituido.getSimbolo() == this.simbolo
            ? substituto
            : this;

}

public class Soma : Expressao 
{
    Expressao a, b;
    
    public Soma(Expressao x, Expressao y) 
    {
        this.a = x;
        this.b = y;
    }

    public override string ToString() => $"({a.ToString()} + {b.ToString()})";

    public override Expressao Derivar(Simbolo x) =>
        new Soma(a.Derivar(x), b.Derivar(x));

    public override Expressao Simplificar() 
    {
        if (a is Numero && (a as Numero).getValor() == 0) 
        {
            return this.b;
        } 
        else if (b is Numero && (b as Numero).getValor() == 0)
        {
            return this.a;
        } 
        else if (a is Numero && b is Numero )
        {
            return new Numero((a as Numero).getValor() + (b as Numero).getValor());
        } 
        else 
        {
            return this;
        }
    }

    public override Expressao Substituir(Simbolo substituido, Expressao substituto)  
    {
        if (a is Simbolo && substituido.getSimbolo() == (a as Simbolo).getSimbolo()) 
        {
            return new Soma(substituto, this.b);
        } 
        else if (b is Simbolo && substituido.getSimbolo() == (b as Simbolo).getSimbolo())
        {
            return new Soma(this.a, substituto);
        }
        else
        {
            return this;
        } 
    }
}

public class Subtracao : Expressao 
{

    Expressao a, b;
    
    public Subtracao(Expressao x, Expressao y) 
    {
        this.a = x;
        this.b = y;
    }

    public override string ToString() => $"({a.ToString()} - {b.ToString()})";

    public override Expressao Derivar(Simbolo x) =>
        new Subtracao(a.Derivar(x), b.Derivar(x));

    public override Expressao Simplificar() 
    {
        if (a is Numero && (a as Numero).getValor() == 0) 
        {
            return this.b;
        } 
        else if (b is Numero && (b as Numero).getValor() == 0)
        {
            return this.a;
        } 
        else if (a is Numero && b is Numero )
        {
            return new Numero((a as Numero).getValor() - (b as Numero).getValor());
        } 
        else 
        {
            return this;
        }
    }

    public override Expressao Substituir(Simbolo substituido, Expressao substituto) 
    {
        if (a is Simbolo && substituido.getSimbolo() == (a as Simbolo).getSimbolo()) 
        {
            return new Subtracao(substituto, this.b);
        } 
        else if (b is Simbolo && substituido.getSimbolo() == (b as Simbolo).getSimbolo())
        {
            return new Subtracao(this.a, substituto);
        }
        else
        {
            return this;
        } 
    }
}

public class Multiplicao : Expressao 
{

    Expressao a, b;
    
    public Multiplicao(Expressao x, Expressao y) 
    {
        this.a = x;
        this.b = y;
    }

    public override string ToString() => $"({a.ToString()} * {b.ToString()})";

    public override Expressao Derivar(Simbolo x) =>
        new Soma(
            new Multiplicao(a.Derivar(x), b),
            new Multiplicao(a, b.Derivar(x)));

    public override Expressao Simplificar() 
    {
        if (a is Numero && (a as Numero).getValor() == 0 || b is Numero && (b as Numero).getValor() == 0) 
        {
            return new Numero(0);
        }
        else if (a is Numero && b is Numero )
        {
            return new Numero((a as Numero).getValor() * (b as Numero).getValor());
        } 
        else 
        {
            return this;
        }
    }

    public override Expressao Substituir(Simbolo substituido, Expressao substituto)  
    {
        if (a is Simbolo && substituido.getSimbolo() == (a as Simbolo).getSimbolo()) 
        {
            return new Multiplicao(substituto, this.b);
        } 
        else if (b is Simbolo && substituido.getSimbolo() == (b as Simbolo).getSimbolo())
        {
            return new Multiplicao(this.a, substituto);
        }
        else
        {
            return this;
        } 
    }
}

public class Divisao : Expressao 
{

    Expressao a, b;
    
    public Divisao(Expressao x, Expressao y) 
    {
        this.a = x;
        this.b = y;
    }

    public override string ToString() => $"({a.ToString()} / {b.ToString()})";

    public override Expressao Derivar(Simbolo x) =>
        new Divisao (
            new Subtracao (
                new Multiplicao(a.Derivar(x), b),
                new Multiplicao(a, b.Derivar(x))),
            new Multiplicao(b, b));

    public override Expressao Simplificar() 
    {
        if (a is Numero && (a as Numero).getValor() == 0 || b is Numero && (b as Numero).getValor() == 0) 
        {
            return new Simbolo("Indefinido");
        }
        else if (a is Numero && b is Numero )
        {
            return new Numero((a as Numero).getValor() / (b as Numero).getValor());
        } 
        else 
        {
            return this;
        }
    }

    public override Expressao Substituir(Simbolo substituido, Expressao substituto)  
    {
        if (a is Simbolo && substituido.getSimbolo() == (a as Simbolo).getSimbolo()) 
        {
            return new Divisao(substituto, this.b);
        } 
        else if (b is Simbolo && substituido.getSimbolo() == (b as Simbolo).getSimbolo())
        {
            return new Divisao(this.a, substituto);
        }
        else
        {
            return this;
        } 
    }
}

public class NumeroComplexo : Expressao 
{

    int real, imag;

    public NumeroComplexo(int r, int i) 
    {
        this.real = r;
        this.imag = i;
    }

    public override string ToString() => 
        imag > 0
            ? $"({real.ToString()} + {imag.ToString()}i)"
            : $"({real.ToString()}{imag.ToString()}i)";

    public override Expressao Derivar(Simbolo x) => new Numero(0);

    public override Expressao Simplificar() => this;

    public override Expressao Substituir(Simbolo substituido, Expressao substituto) => this;
}
