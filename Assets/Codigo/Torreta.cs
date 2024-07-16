using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public SphereCollider ColisionDeteccion;
    public float RangoDeteccion;
    public GameObject Objetivo, SalidaBala;
    public Bala Bala;
    public Transform ObjetoARotar;
    public float Temporizador;
    public float TiempoEntreDisparos=1f;

    public List<Collider> ObjetosAIgnorarBala;

    void Start()
    {
        //Defino el rango de la colision de deteccion de forma apropiada
        ColisionDeteccion.radius = RangoDeteccion;
    }

  
    void Update()
    {
        //Ejecuto lo que la torreta haga en update
        Comportamiento();
    }

    private void Comportamiento()
    {
        if(Objetivo)
        {
            //Mira hacia el objetivo objeto a rotar
            Vector3 Direccion = Objetivo.transform.position-ObjetoARotar.position;
            Quaternion NuevaRotacion = Quaternion.LookRotation(Direccion);
            ObjetoARotar.transform.rotation = Quaternion.Lerp(ObjetoARotar.transform.rotation,NuevaRotacion,Time.deltaTime*10);
            //ObjetoARotar.LookAt(Objetivo.transform.position);
            ComprobarSiPuedoDisparar();
        }
        SistemaTiempo();
    }

    void SistemaTiempo()
    {
        Temporizador += Time.deltaTime;
    }

    void ComprobarSiPuedoDisparar()
    {
        if (Temporizador >= TiempoEntreDisparos)
        {
            Disparar();
            Temporizador = 0;
        }
    }
    void Disparar()
    {
        //Creo la bala
        Bala BalaNueva = Instantiate(Bala, SalidaBala.transform.position, ObjetoARotar.transform.rotation);
        //Definimos su dueño
        BalaNueva.Dueño = this;
        //añado la colision a la lista
        ObjetosAIgnorarBala.Add(BalaNueva.Colision);
        //Cancelo las colisiones
        CancelarColisiones(BalaNueva.Colision);
    }
    public void CancelarColisiones(Collider bala)
    {
        for (int i = 0; i < ObjetosAIgnorarBala.Count; i++)
        {
            Physics.IgnoreCollision(bala, ObjetosAIgnorarBala[i]);
        }
    }
    public void LimpiarDeLista(Collider bala)
    {
        ObjetosAIgnorarBala.Remove(bala);
    }
    private void DefinirObjetivo(GameObject objeto)
    {
        Objetivo = objeto;
    }

    private void OnTriggerEnter(Collider objetoColisionado)
    {
       ITieneVida Dañable = objetoColisionado.GetComponent<ITieneVida>();
        if(Dañable!=null)
        {
            DefinirObjetivo(objetoColisionado.gameObject);
        }
        //Compruebo si con lo que he colisionado tiene vida
        //si es asi lo defino como objetivo

    }

    private void OnTriggerExit(Collider objetoColisionado)
    {
        //Si es el mismo que nuestro objetivo
        if (objetoColisionado.gameObject == Objetivo)
        {
            //Objetivo es null
            DefinirObjetivo(null);
        }
    }

}
