using UnityEngine;

public class GestorAmbiental : MonoBehaviour
{
    public static GestorAmbiental Instancia;

    public delegate void Accion(Color color);
    public static event Accion AlCambiarcolor;
    public Color ColorAmbiental;


    private void Awake()
    {
        Instancia = this;
    }

    public void PonerColorAmbiental(Color color)
    {
        ColorAmbiental = color;
        //Cambiamos el color de la iluminacion ambiental
        RenderSettings.ambientLight = ColorAmbiental/2;
        //Cambiamos el color de fondo de la camara

        //Cambiamos el color de la niebla
        RenderSettings.fogColor = ColorAmbiental / 4;
        if(AlCambiarcolor != null)
        {
            AlCambiarcolor(color);
        }
    }
}
