public interface IObservadorPedido
{
	void AoMudarStatusPedido(Pedido pedido, string novoStatus);
}

public class Pedido
{
	private List<IObservadorPedido> _observadores = new List<IObservadorPedido>();
	private string _status;
   
	public string Status
	{
    	    get => _status;
    	    set
    	    {
        	   _status = value;
        	   NotificarObservadores();
    	    }
	}
   
	public void Inscrever(IObservadorPedido observador)
	{
    	    _observadores.Add(observador);
	}
   
	private void NotificarObservadores()
	{
    	    foreach (var observador in _observadores)
    	    {
        	    observador.AoMudarStatusPedido(this, _status);
    	    }
	}
}

public class NotificadorEmail : IObservadorPedido
{
	public void AoMudarStatusPedido(Pedido pedido, string novoStatus)
	{
		Console.WriteLine($"Enviando e‑mail...");
		Console.WriteLine($"Status alterado para {novoStatus}.");
		Console.WriteLine("E‑mail enviado!\n");
	}
}

public class NotificadorSMS : IObservadorPedido
{
    public void AoMudarStatusPedido(Pedido pedido, string novoStatus)
    {
        Console.WriteLine($"Enviando SMS...");
        Console.WriteLine($"Seu pedido agora está {novoStatus}.");
        Console.WriteLine("SMS enviado!\n");
    }
}

