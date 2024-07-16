using UnityEngine;

public class CuboFiesta : MonoBehaviour,IColisionable
{
    Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;    
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Hago que el cubo sea invisible
        material.color = Vector4.zero;
    }

    public void CambiarColor()
    {
        //Cambia a un color aleatorio
        material.color = ColorAleatorio();
        material.SetColor(Atajos.Emision, material.color*2);
    }

    Color ColorAleatorio()
    {
        Color colorTemporal= new Color(NumeroAleatorio(), NumeroAleatorio(), NumeroAleatorio());

        //si el color es negro, pasamos a gris muy oscuro
        if(colorTemporal.maxColorComponent <0.001f)
        {
            colorTemporal= Color.white*0.001f;
        }
        //Incremento el color hasta que el valor maximo alcance el uno
        colorTemporal/= colorTemporal.maxColorComponent;
        colorTemporal.a = 0.1f;
        return colorTemporal;
    }
    float NumeroAleatorio()
    {
       return Random.value;
    }

    public void AlColisionar(GameObject Objeto)
    {
        //Cambio el color del cubo
        CambiarColor();
        //Cambio la luz ambiental
        GestorAmbiental.Instancia.PonerColorAmbiental(material.color);
    }
}
