using UnityEngine;

public class GestorAparicion : MonoBehaviour
{
    public static GestorAparicion Instancia;
    public GameObject Personaje;
    public GameObject[] PuntosDeControl;


    private void Awake()
    {
        Instancia = this;
    }

    private void Start()
    {
        CrearPersonaje(0);
    }
    public void CrearPersonaje(int ID)
    {
        GameObject PersonajeNuevo = Instantiate(Personaje);
        Personaje = PersonajeNuevo;
        MoverAPunto(ID);
    }

    public void MoverAPunto(int ID)
    {
Personaje.transform.position= PuntosDeControl[ID].transform.position;
        //Aqui podriamos cambiar la camara
    }
    public void Reaparecer()
    {
        float DistanciaActual = 1000f;
        int ID = 0;
        //Paso por los puntos de control
        for (int i = 0; i < PuntosDeControl.Length; i++)
        {
            //Calculo la distancia al punto actual
            float DistanciaAPunto = Vector3.Distance(Personaje.transform.position, PuntosDeControl[i].transform.position);
            //comprueba si la distancia es menor a la actual
            if(DistanciaActual > DistanciaAPunto)
            {
                //Este es el punto que esta mas cerca, debo guardarlo
                ID = i;
                //Actualizo la distancia actual
                DistanciaActual = DistanciaAPunto;
            }
        }
        MoverAPunto(ID);
    }
    private void OnEnable()
    {
        GestorDebug.ModoDebug += ModoDebug;
    }

    private void OnDisable()
    {
        GestorDebug.ModoDebug -= ModoDebug;
    }
    private void ModoDebug(bool opcion)
    {
        for (int i = 0; i < PuntosDeControl.Length; i++)
        {
            if(PuntosDeControl[i].TryGetComponent(out Renderer renderer))
            {
            renderer.enabled = opcion;
            }
        }
    }
}
