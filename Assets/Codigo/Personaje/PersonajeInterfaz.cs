using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeInterfaz : MonoBehaviour
{
    public Slider SliderVida;
    public TMP_Text TextoMonedas;

    public void ActualizarSliderVida(Estadisticas estadisticas)
    {
        SliderVida.maxValue = estadisticas.VidaMaxima;
        SliderVida.value = estadisticas.VidaActual;
    }

    public void ActualizarTextoMonedas(Inventario inventario)
    {
        //Actualizar texto
        int valor = inventario.MonedasMaximas.ToString().Length; //Cantidad de numeros que hay en monedas
        string adaptacion = "";//Creo string vacio
        for (int i = 0; i < valor; i++) 
        {
            adaptacion += 0; //Por cada nunmero, añado un 0
        }
        TextoMonedas.text = string.Format("{0:" + adaptacion + "}", inventario.Monedas);
    }
}
