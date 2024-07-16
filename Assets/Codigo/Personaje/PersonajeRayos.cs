using UnityEngine;


public class PersonajeRayos : MonoBehaviour
{
    [Header("Referencias")]
    public Collider Colision;
    public Transform Camara;
    public RaycastHit DatosPendiente;
    [Header("Interacciones")]
    public float RangoDeteccionSuelo;
    public Color ColorDeteccionSuelo;
    public bool EnSuelo;
    public float RangoInteraccion;
    public Color ColorInteraccion;
    public float RangoMirar=4;
    public float DistanciaRayoEscalada; //Distancia del rayo que detecta la angulacion con el suelo
    public float AnguloEscaladaMaximo=50;
    public float AngulacionSuelo;
    [Header("Medidas")]
    public float Alto;
    public float Ancho;
    public float Radio;

    private void Awake()
    {
        Colision = GetComponent<Collider>();
    }
  
    void Start()
    {
        CalcularMedidas();
        
    }

    
    void Update()
    {
        DetectarSuelo();
        Mirar();
    }
    public void DetectarSuelo()
    {
        RaycastHit Datos; //Aqui se guarda la informacion del raycast
        //if(Physics.SphereCast(Origen,radio,direccion,salida,distancia))
        if(Physics.SphereCast(transform.position,Radio,-transform.up,out Datos,RangoDeteccionSuelo))
        {
            Debug.DrawRay(transform.position, -transform.up * (Datos.distance + Radio), ColorDeteccionSuelo);
            //Comprobamos si estamos en una pendiente
            if (Physics.Raycast(transform.position,-transform.up, out DatosPendiente,DistanciaRayoEscalada))
            {
                //Cogemos el angulo de la pendiente usando su normal
                AngulacionSuelo = Vector3.Angle(transform.up, DatosPendiente.normal);
            }
            //Estamos en el suelo si AngulacionSuelo es menor a el angolo de escalada maximo
            EnSuelo = AngulacionSuelo <= AnguloEscaladaMaximo;
            //Dibujo el rayo de escalada
            Debug.DrawRay(transform.position + transform.forward * 0.01f, -transform.up * DistanciaRayoEscalada, Color.magenta);
        }
        else
        {
            EnSuelo = false;
            Debug.DrawRay(transform.position, -transform.up * (RangoDeteccionSuelo + Radio), Color.green);
        }
    }

    public void Interaccion()
    {
        RaycastHit Datos;
        //Raycast(Origen,Direccion,Salida,Rango)
        //Raycast(Rayo,Salida,Rango)
        if(Physics.Raycast(Camara.position,Camara.forward, out Datos,RangoInteraccion))
        {
            Debug.DrawRay(Camara.position, Camara.forward * Datos.distance , ColorInteraccion,10f);
            IInteractuable ObjetoInteractuable = Datos.transform.GetComponent<IInteractuable>();
            if (ObjetoInteractuable!=null)
            {
                ObjetoInteractuable.AlInteractuar();
            }
        }
    }

    void Mirar()
    {
        RaycastHit Datos;
        if (Physics.Raycast(Camara.position, Camara.forward, out Datos, RangoInteraccion))
        {
            IMirable ObjetoMirable = Datos.transform.GetComponent<IMirable>();
            if (ObjetoMirable != null)
            {
                ObjetoMirable.AlMirar();
            }
        }
    }
    public void CalcularMedidas()
    {
        Alto = Colision.bounds.size.y;
        Ancho = Colision.bounds.size.x;
        Radio = Ancho / 2;
        RangoDeteccionSuelo = Alto / 2 - Radio + 0.001f;
        DistanciaRayoEscalada = Radio / Mathf.Sin((90 - AnguloEscaladaMaximo) * Mathf.PI / 180) + Alto / 2 - Radio + 0.001f;
    }
}
