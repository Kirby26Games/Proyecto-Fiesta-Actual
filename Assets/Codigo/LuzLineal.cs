using UnityEngine;

public class LuzLineal : MonoBehaviour,IMirable
{
    Material Material;
    Light Luz;
    public Color ColorInicial;
    public Color ColorFinal;
    public float Intensidad;
    public float LimiteIntensidad;
    public float TiempoMaximo;
    public float Incremento; //Cantidad que cambia para llegar al maximo en el tiempo Maximo

    private void Awake()
    {
        Luz=GetComponent<Light>();
        Material=GetComponent<Renderer>().material;
    }

    void Start()
    {
        Inicio();
    }

    void Inicio()
    {
        Material.color = ColorFinal;
        Intensidad = 0;
        AlMirar();
    }

    public void AlMirar()
    {
        if(Intensidad>=LimiteIntensidad)
        {
            return;
        }
        //Al mirarlo, se cambiara entre el color al empeza, y el color final
        Incremento=LimiteIntensidad/TiempoMaximo;   //Lo que tiene que aumentar por segundo para llegar al maximo de intensidad en el tiempo
        Intensidad += Incremento * Time.deltaTime;
        Intensidad = Mathf.Clamp(Intensidad, 0, LimiteIntensidad);
        CalcularColor();
    }

    public void CalcularColor()
    {
        Material.SetColor(Atajos.Emision, Color.Lerp(ColorInicial, ColorFinal, Intensidad / LimiteIntensidad) * 4);
        Luz.color = Color.Lerp(ColorInicial, ColorFinal, Intensidad / LimiteIntensidad);
    }
    public void CambiarColor(Color color)
    {
        ColorFinal = color;
        CalcularColor();
    }

    private void OnEnable()
    {
        GestorAmbiental.AlCambiarcolor += CambiarColor;
    }
    private void OnDisable()
    {
        GestorAmbiental.AlCambiarcolor -= CambiarColor;
    }
}
