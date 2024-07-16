using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour,IActivable,ITieneDue�o
{
    public float Velocidad = 1f;
    public Vector3[] Posiciones;
    public bool DeboMoverme;
    public bool PuedoCambiar;
    public bool MovimientoContinuo;
    public int Indice;
    Boton Due�o;

    void Update()
    {
        Movimiento();
    }
    void Movimiento()
    {
        if (!DeboMoverme)
        {
            return;
        }
        //Aqui debo moverme hacia la direccion objetivo a la Velocidad
        //MoveTowards mueve en direccion a un objeto(Origen,Destino,Velocidad)
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Posiciones[Indice],Velocidad*Time.deltaTime);
        CalcularDistancia();
    }

    public void CalcularDistancia()
    {
        //si he lllegado a la posicion objetivo, dejo de poder moverme y continuo
        if (Vector3.Distance(transform.localPosition, Posiciones[Indice])<=0.01f)
        {
            transform.localPosition = Posiciones[Indice];
            if(!MovimientoContinuo)
            {
            DeboMoverme = false;
            }
            Continuar();
        }
    }
    public void AlActivar()
    {
        if(PuedoCambiar&&DeboMoverme)
        {
            Continuar();
        }
        DeboMoverme = true;
    }

    public void Continuar()
    {
        Indice++;
        if(Indice>=Posiciones.Length)
        {
            Indice = 0;
        }
        if(Due�o!=null)
        {
            Due�o.HaLlegadoUnObjeto();
        }
    }
    public void DefinirDue�o(GameObject due�o)
    {
        Due�o = due�o.GetComponent<Boton>();
    }

}
