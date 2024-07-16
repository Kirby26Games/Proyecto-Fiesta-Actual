using UnityEngine;
[RequireComponent (typeof(Rigidbody),typeof(Light))]
public class Pelota : MonoBehaviour,IInteractuable
{
    [Header("Referencias")]
    public Rigidbody RigidBody;     //Rigidbody de la pelota, sin el no tenemos fisicas
    public Material MaterialBola;   //Material al que cambiaremos de color
    public Light Luz;
    [Header("Informacion")]
    public float FuerzaActual;      //Fuerza actual acumulada
    public float FuerzaFinal;       //Fuerza final, teniendo en cuenta el peso del objeto
    private bool Activada;          //Si la pelota esta activada
    private int TirosActuales;
    [Header("Configuracion")]
    public float FuerzaMaxima = 10;             //Fuerza maxima a la que podemos llegar
    public float TiempoHastaFuerzaMaxima = 5;   //Tiempo que tardamos en llegar a a dicha fuerza
    public Color ColorInicial = Color.white;    //Color al empezar
    public Color ColorFinal = Color.black;      //Color al llegar a la fuerza maxima
    public int TirosMaximos = 1;                //Veces maximas que se puede lanzar


    //Extra: en vez de reiniciarse la fuerza, vuelve a 0 de forma lineal en un segundo.


    private void Awake()
    {
        Luz = GetComponent<Light>();
        MaterialBola = GetComponent<Renderer>().material;
        RigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Luz.range = 2;
        Luz.renderMode= LightRenderMode.ForcePixel;
        MaterialBola.color = ColorInicial;
        Activada = false;
        FuerzaActual = 0;
        Interpolacion(0);
    }
    private void Update()
    {
        Cargar();
    }
    public void AlInteractuar()
    {
        Activar();
       //Activo la pelota
    }

    public void Activar()
    {
        if(TirosActuales>=TirosMaximos)
        {
            return;
        }
        if(Activada)
        {
            Soltar();
        }
        Activada = !Activada;
    }

    public void Soltar()
    {
        FuerzaFinal = FuerzaActual * RigidBody.mass * 100;
        RigidBody.AddForce(Vector3.Cross(transform.right, Vector3.up) * FuerzaFinal);
        TirosActuales++;
        FuerzaActual = 0;
    }

    public void Cargar()
    {
        if(!Activada)
        {
            return;
        }
        if(FuerzaActual>FuerzaMaxima)
        {
            FuerzaActual = 0;
            return;
        }
        //Se va cargando la pelota, se carga cada segundo lo necesario para llegar al maximo de fuerza
        //En el tiempo maximo
        float fuerzaporsegundo = FuerzaMaxima / TiempoHastaFuerzaMaxima;
        FuerzaActual += fuerzaporsegundo * Time.deltaTime;
        Interpolacion(FuerzaActual / FuerzaMaxima);
    }

    public void Interpolacion(float porcentaje)
    {
        //En este caso, lo haremos con colores
        MaterialBola.color = Color.Lerp(ColorInicial, ColorFinal, porcentaje);
        MaterialBola.SetColor(Atajos.Emision, MaterialBola.color * 4);
        Luz.color = MaterialBola.color;
    }
}
