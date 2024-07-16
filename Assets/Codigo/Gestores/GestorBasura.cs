using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GestorBasura : MonoBehaviour
{
    public static async Task EliminarEnTiempo(GameObject objeto,float tiempo)
    {
        Limpiar(objeto);
        await Task.Delay((int)(tiempo*1000));
        Eliminar(objeto);
    }

    private static void Limpiar(GameObject objeto)
    {
        Collider collider = objeto.GetComponent<Collider>();
        if(collider)
        {
            collider.enabled = false;
        }
        Renderer renderer = objeto.GetComponent<Renderer>();
        if (renderer)
        {
            renderer.enabled = false;
        }
        Light luz = objeto.GetComponent<Light>();
        if (luz)
        {
            luz.enabled = false;
        }
        //Nos aseguramos de hacer lo mismo en todos los hijos del objeto
        for (int i = 0; i < objeto.transform.childCount; i++)
        {
            Limpiar(objeto.transform.GetChild(i).gameObject);
        }
    }

    private static void Eliminar(GameObject objeto)
    {
        Destroy(objeto);
    }


}
