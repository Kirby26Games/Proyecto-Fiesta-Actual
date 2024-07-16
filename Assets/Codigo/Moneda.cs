using UnityEngine;

public class Moneda : Consumible
{
    public int Valor;
    public override void AlConsumir(GameObject consumidor)
    {
        ITieneMonedas Monedero = consumidor.GetComponent<ITieneMonedas>();
        if (Monedero != null)
        {
            Monedero.ModificarMonedas(Valor);
        }
        ReproducirAudio();
        Eliminar();
    }
}
