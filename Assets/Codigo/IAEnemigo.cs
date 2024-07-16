using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemigo : MonoBehaviour
{
    NavMeshAgent Agente;

    [Header("IA")]
    public Animator Animador;
    public GameObject Objetivo;//Lo que persigue
    public Vector3[] PuntosDeControl;//Puntos que va a seguir
    public Vector3 PuntoActual;//Posicion a la que se dirije
    public SphereCollider ColisionDeteccion, ColisionAtaque;//Colisiones para interacciones

    [Header("Informacion")]
    public int Indice;//Punto de control al que esta yendo
    public float VelocidadActual;
    public float TiempoQuieto;
    public float TiempoAtacando;

    [Header("Opciones")]
    public bool RutaAleatoria;
    public int Daño; //Daño que hara al personaje
    public float RangoDeteccion,RangoAtaque;//Rango al que nos detectara
    public float VelocidadAndar, VelocidadPerseguir;
    public float MaximoTiempoQuieto=5f;
    public float MaximoTiempoAtacando=2f;

    Vector3 PosicionAnterior;

    private void Awake()
    {
        Agente= GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        ColisionDeteccion.transform.localScale = Vector3.one*RangoDeteccion*2;
        ColisionAtaque.transform.localScale = Vector3.one * RangoAtaque * 2;
        Agente.speed = VelocidadAndar;
        TiempoAtacando = MaximoTiempoAtacando;
        CambiarPuntoDecontrol();
    }
    private void Update()
    {
        Comportamiento();
    }
    public void Comportamiento()
    {
        //si el enemigo se mueve a menos de x velocidad
        //aumentamos tiempo quieto
        //Si tiempo quieto es mas que maximo tiempo quieto
        //Pasa al siguiente punto de control
        VelocidadActual = (transform.position - PosicionAnterior).magnitude / Time.deltaTime;
        PosicionAnterior=transform.position;
        Animador.SetFloat("Velocidad",VelocidadActual);
        if(Atacando())
        {
            return;
        }
        if(Persiguiendo())
        {
            Perseguir();
        }
        else
        {
            Patrullar();
        }
        ComprobarTiempoQuieto();
    }

    public void ComprobarTiempoQuieto()
    {
        if(VelocidadActual<=0.5)
        {
            TiempoQuieto += Time.deltaTime;
        }
        else
        {
            TiempoQuieto = 0;
        }
        if(TiempoQuieto>=MaximoTiempoQuieto)
        {
            DefinirObjetivo(null);
            CambiarPuntoDecontrol();
            TiempoQuieto = 0;
        }
    }
   
    public void ComprobarDistancia()
    {
        //Este 10, debe ser la mitad de la distancia entre un piso y otro de nuestro juego
        //Si la distancia al lugar actual es menor a 1, cambio de punto de control
        bool CercaDePunto= Vector3.Distance(transform.position, PuntoActual) < 10;
        Vector3 PuntoActualSinAltura = new Vector3(PuntoActual.x, transform.position.y, PuntoActual.z);
        bool CercaIgnorandoAltura= Vector3.Distance(transform.position,PuntoActualSinAltura) < 0.2f;
        if(CercaDePunto&&CercaIgnorandoAltura)
        {
            CambiarPuntoDecontrol();
        }
    }
    public void CambiarPuntoDecontrol()
    {
        Agente.speed= VelocidadAndar;
        if(RutaAleatoria==true)
        {
            Indice= Random.Range(0,PuntosDeControl.Length);
        }
        else
        {
            Indice++;
            if(Indice>=PuntosDeControl.Length)
            {
                Indice = 0;
            }
        }
        PuntoActual = PuntosDeControl[Indice];
    }

    public void DefinirObjetivo(GameObject objetivo)
    {
        Agente.speed = VelocidadPerseguir;
        Objetivo= objetivo;
    }
    public bool Atacando()
    {
        //Animador.SetFloat("Velocidad", 0);
        Agente.destination = Agente.transform.position;
        TiempoAtacando += Time.deltaTime;
        return TiempoAtacando < MaximoTiempoAtacando;
    }
    public bool Persiguiendo()
    {
        return Objetivo != null;
    }
    public void Perseguir()
    {
        
        //Animador.SetFloat("Velocidad", VelocidadPerseguir);
        Agente.destination = Objetivo.transform.position;       
    }

    public void Patrullar()
    {
        //Animador.SetFloat("Velocidad", VelocidadAndar);
        Agente.destination = PuntoActual;
        ComprobarDistancia();
    }

    public void Atacar(GameObject objetivo)
    {
        Animador.SetTrigger("Atacar");
        //Aqui haremos daño al personaje y pondremos a animacion de atacar
        TiempoAtacando = 0;
        ITieneVida Dañable = objetivo.GetComponent<ITieneVida>();
        if(Dañable != null)
        {
            Dañable.ModificarVidaPerdida(Daño);
            Objetivo = null;
        }
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
        ColisionAtaque.GetComponent<Renderer>().enabled = opcion;
        ColisionDeteccion.GetComponent<Renderer>().enabled = opcion;
    }
}
