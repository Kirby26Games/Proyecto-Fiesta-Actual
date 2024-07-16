using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class CogerObjetos : MonoBehaviour
{
    public Transform Camara;
    public Transform Mano;
    public Movible ObjetoActual;
    public float RangoAccion;
    public bool ComprobandoTecla;
    public KeyCode[] _keyCodes = Enum.GetValues(typeof(KeyCode)) as KeyCode[];
    private void Update()
    {
        Prueba();
        Controles();
    }
    private void Prueba()
    {
        
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in _keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    Debug.Log("Normal:"+keyCode);
                   Idioma.Saltar= keyCode;
                    break;
                }
            }
        }
    }
    private void OnGUI()
    {
        if(ComprobandoTecla)
        {
            Event evento = Event.current;
            if (evento.type == EventType.KeyDown)
            {
                Debug.Log("Gui:" + evento.keyCode);
               Idioma.Saltar= evento.keyCode;
                ComprobandoTecla = false;
            }
        }
    }

    private void Controles()
    {
        if(Input.GetMouseButton(1)) 
        {
            BuscarObjeto();
            AtraerObjeto();
        }
        if (Input.GetMouseButtonUp(1))
        {
            SoltarObjeto();
        }
    }
    private void BuscarObjeto()
    {
        if (!Physics.Raycast(Camara.position, Camara.forward, out RaycastHit Datos, RangoAccion))
        {
            return;
        }
        Movible objetoMovible = Datos.transform.GetComponent<Movible>();
        if (objetoMovible == null)
        {
            return;
        }
        if (ObjetoActual != null && objetoMovible != ObjetoActual)
        {
            SoltarObjeto();
        }
        ObjetoActual = objetoMovible;
    }
    private void AtraerObjeto()
    {
        if (ObjetoActual != null)
        {
            ObjetoActual.MoverHacia(Mano);
        }
    }
    private void SoltarObjeto()
    {
        if (ObjetoActual != null)
        {
            ObjetoActual.Liberar();
            ObjetoActual = null;
        }
    }
}
