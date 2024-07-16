using UnityEngine;

public class Curacion : Consumible
{
    public int Vida;

    public override void AlConsumir(GameObject consumidor)
    {
        ITieneVida ObjetoConVida = consumidor.GetComponent<ITieneVida>();
        if(ObjetoConVida != null )
        {
            ObjetoConVida.ModificarVidaPerdida(-Vida);
        }

        ReproducirAudio();
        Eliminar();
    }
}
