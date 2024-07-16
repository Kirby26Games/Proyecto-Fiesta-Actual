using JetBrains.Annotations;
using System;
using UnityEngine;

public struct Atajos 
{
    public static readonly string Emision = "_EmissionColor";
}
public struct Idioma
{
    public static string Monedas = "Monedas";
    public static KeyCode Saltar = KeyCode.S;

}

[Serializable]
public struct Estadisticas
{
    public int Nivel;
    public int Constitucion;

    public int VidaActual;
    public int VidaMaxima;
    public int VidaPerdida;

    public void CalcularVida()
    {
        VidaMaxima= 10+(10*Nivel)+(Constitucion*Nivel);
        VidaPerdida = Mathf.Clamp(VidaPerdida, 0, VidaMaxima);
        VidaActual = VidaMaxima - VidaPerdida;
        if(VidaActual<=0)
        {
            //estoy morido
        }
    }

    public void ModificarVidaPerdida(int cantidad)
    {
        VidaPerdida += cantidad;
        CalcularVida();
    }
}

[Serializable]
public struct Inventario
{
    public int Monedas;
    public int MonedasMaximas;

    void CalcularMonedas()
    {
        Monedas= Mathf.Clamp(Monedas,0,MonedasMaximas);
    }

    public void ModificarMonedas(int cantidad)
    {
        Monedas+= cantidad;
        CalcularMonedas();
    }


}

[Serializable]
public struct ArchivoGuardado
{
    public Sistemas Sistemas;
    public Personaje Personaje;
}
[Serializable]
public struct Personaje
{
    public Perfil Perfil;
    public Estadisticas Estadisticas;
    public Inventario Inventario;
    public Vector3 Posicion;
    public Vector3 Rotacion;
}
[Serializable]
public struct Perfil
{
    public string Nombre;
}
[Serializable]
public struct Sistemas
{
    public int Escena;
    public Color ColorAmbiental;
}