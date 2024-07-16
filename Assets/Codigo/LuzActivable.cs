using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzActivable : MonoBehaviour,IActivable
{
    Material Material;
    Light Luz;
    Color ColorInicial;
    public Color ColorFinal;
    public float Estado;

    private void Awake()
    {
        //Cojo el material
        Material = GetComponent<Renderer>().material;
        //cojo la luz
        Luz= transform.GetChild(0).GetComponent<Light>();
    }
    private void Start()
    {
        Inicio();
    }
    public void Inicio()
    {
        //El Color Inicial es Blanco*0.01f;
        ColorInicial= Color.white*0.01f;
        //Llamo a modificar con porcentaje 0
        ModificarLuz(0);
    }
    public void AlActivar()
    {
        //Enciendo la luz
        EncenderLuz();
    }
    public void EncenderLuz()
    {
        //Llamo a modificar con porcentaje 1
        ModificarLuz(1);
    }
    public void ModificarLuz(float porcentaje)
    {
        Estado= porcentaje;
        //Pongo el color de la luz segun el lerp entre color minimo y maximo
        Luz.color =Color.Lerp(ColorInicial,ColorFinal,porcentaje);
        //Pongo e color del emisivo del material igual a *4
        Material.SetColor(Atajos.Emision, Luz.color * 4);
        Material.color = ColorFinal;
    }

    public void CambioColor(Color color)
    {
        ColorFinal = color;
        ModificarLuz(Estado);
    }

    private void OnEnable()
    {
        GestorAmbiental.AlCambiarcolor += CambioColor;
    }
    private void OnDisable()
    {
        GestorAmbiental.AlCambiarcolor -= CambioColor;
    }

}
