
using UnityEngine;

public interface IActivable
{
    public void AlActivar();
}
public interface IInteractuable
{
    public void AlInteractuar();
}
public interface IMirable
{
    public void AlMirar();
}

public interface IColisionable
{
    public void AlColisionar(GameObject Objeto);
}

public interface ITieneVida
{
    public void ModificarVidaPerdida(int cantidad);
}



public interface ITieneMonedas
{
    public void ModificarMonedas(int cantidad); 
}

public interface IConsumible
{
    public void AlConsumir(GameObject consumidor);
}
public interface ITieneDueño
{
    public void DefinirDueño(GameObject dueño);
}