using System.Threading.Tasks;
using UnityEngine;

public abstract class Consumible : MonoBehaviour,IConsumible
{
    public abstract void AlConsumir(GameObject consumidor);

    public void ReproducirAudio()
    {
        ReproductorAudio Reproductor = GetComponent<ReproductorAudio>();
        if (Reproductor != null)
        {
            Reproductor.PonerAudio();
        }
    }

    public async void Eliminar()
    {
        await GestorBasura.EliminarEnTiempo(gameObject, 2f);
    }

}
