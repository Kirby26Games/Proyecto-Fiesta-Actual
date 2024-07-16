
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour,IInteractuable
{
    [Header("Referencias")]
    public GameObject[] ObjetosActivables;
    public Renderer ObjetoMaterial;
    public Material Material;
    public Light Luz;
    [Header("Opciones")]
    public bool DeboEsperar;
    public bool UsoPesos;
    [Header("Datos")]
    public bool Activado;
    public int DebenLlegar; //Objetos que deben llegar a su destino
    public int HanLlegado; //Objetos que han llegado
    public float PesoParaActivarme;
    public float PesoActual;

    private void Awake()
    {
       Material= ObjetoMaterial.material; 
        Luz= transform.GetChild(0).GetComponent<Light>();
    }
    private void Start()
    {
        Luz.color = Material.color;
        //Compruebo cuantos objetos deben llegar
        DebenLlegar = ObjetosALlegar();
        HanLlegado = DebenLlegar;
    }

    public void AlInteractuar()
    {
        if (UsoPesos)
        {
            return;
        }
        ActivarBoton();
    }

    private void ActivarBoton()
    {
        //Si debo esperar y no han llegado todos los objetos
        if(DeboEsperar && HanLlegadoTodos() == false)
        {
            //No hago nada
            return;
        }
        HanLlegado = 0;
        Activado = !Activado;
        CambiarColores();
        ActivarObjetos();
    }

    public void CambiarColores()
    {
        if(Activado)
        {
            Luz.intensity = 0.5f;
            Material.SetColor(Atajos.Emision, Material.color * 2);
        }
        else
        {
            Luz.intensity = 0.1f;
            Material.SetColor(Atajos.Emision, Material.color / 2);
        }
    }

    public void ActivarObjetos()
    {
        //Pasar por todos los objetos Activables
        for (int i = 0; i < ObjetosActivables.Length; i++)
        {
            IActivable ObjetoActivable = ObjetosActivables[i].GetComponent<IActivable>();
            if(ObjetoActivable != null )
            {
                ObjetoActivable.AlActivar();
            }    
        }

        //foreach (GameObject objeto in ObjetosActivables)
        //{
        //    IActivable ObjetoActivable = objeto.GetComponent<IActivable>();
        //    if (ObjetoActivable != null)
        //    {
        //        ObjetoActivable.AlActivar();
        //    }
        //}
    }

    private int ObjetosALlegar()
    {
        int cantidad = 0;
        for (int i = 0; i < ObjetosActivables.Length; i++)
        {
            if(ObjetosActivables[i].TryGetComponent<ITieneDueño>(out var objetoConDueño))
            {
                cantidad++;
                objetoConDueño.DefinirDueño(gameObject);
            }
        }
        return cantidad;
    }
    private bool HanLlegadoTodos()
    {
        return HanLlegado >= DebenLlegar;
    }
    public void HaLlegadoUnObjeto()
    {
        HanLlegado++;
    }
    private void ModificarPeso(float peso)
    {
        PesoActual += peso;
        CalcularPeso();
    }
    private void CalcularPeso()
    {
        //Si he llegado al peso y estoy activado, no hago nada
        if(Activado&&PesoActual>= PesoParaActivarme)
        {
            return;
        }

        //si no he llegado al peso y no estoy activado, no hago nada
        if (!Activado && PesoActual < PesoParaActivarme)
        {
            return;
        }
        ActivarBoton();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!UsoPesos)
        {
            return;
        }
        SistemaPeso ObjetoConPeso = other.GetComponent<SistemaPeso>();
        if (ObjetoConPeso != null)
        {
            ModificarPeso(ObjetoConPeso.Peso);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!UsoPesos)
        {
            return;
        }
        SistemaPeso ObjetoConPeso = other.GetComponent<SistemaPeso>();
        if (ObjetoConPeso != null)
        {
            ModificarPeso(-ObjetoConPeso.Peso);
        }
    }

}
