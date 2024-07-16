using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorGuardado : MonoBehaviour
{
    public ArchivoGuardado Guardado;
    public PersonajeSistemas Personaje;

    private void Awake()
    {
        GestorGuardado[] Copias = FindObjectsByType<GestorGuardado>(FindObjectsSortMode.None);
        for (int i = 0; i < Copias.Length; i++)
        {
            if (Copias[i].gameObject!=gameObject)
            {
                Destroy(Copias[i].gameObject);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        Controles(); 
    }
    public void Controles()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GuardarPartida();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            CargarPartida();
        }
    }

    public async void CargarPartida()
    {
        //RUTA
        string ruta = Directory.GetCurrentDirectory() + "/Guardadito.Savecito";

        if(!File.Exists(ruta))
        {
            print("No hay archivo de guardado :(");
            return;
        }
        //Cargo los datos
        Guardado = JsonUtility.FromJson<ArchivoGuardado>(File.ReadAllText(ruta));
        //Cargo escena
        await GestorJuego.CambiarEscenaAsincrona(Guardado.Sistemas.Escena);
        //Cargo iluminacion
        GestorAmbiental.Instancia.PonerColorAmbiental(Guardado.Sistemas.ColorAmbiental);
        //CargoPersonaje
        CargarPersonaje();
    }
    public void CargarPersonaje()
    {
        //Cargo posicion y rotacion
        Personaje.transform.position = Guardado.Personaje.Posicion;
        Personaje.transform.localEulerAngles = Guardado.Personaje.Rotacion.y*Vector3.up;
        Personaje.Camara.RotacionX = Guardado.Personaje.Rotacion.x;
        //CargarNombre
        Personaje.name = Guardado.Personaje.Perfil.Nombre;
        //Cargo estadisticas y actualizo la interfaz
        Personaje.Estadisticas.Estadisticas = Guardado.Personaje.Estadisticas;
        Personaje.Estadisticas.ModificarVidaPerdida(0);
        //Cargo monedas y actualizo interfaz
        Personaje.Inventario.Inventario = Guardado.Personaje.Inventario;
        Personaje.Inventario.ModificarMonedas(0);
    }
    public void GuardarPartida()
    {
        //SceneManager.GetActiveScene().buildIndex; Nos da la escena en la que estamos
        Guardado.Sistemas.Escena= SceneManager.GetActiveScene().buildIndex;

        //Guardamos la luz ambiental
        Guardado.Sistemas.ColorAmbiental = GestorAmbiental.Instancia.ColorAmbiental;

        //Guardamos el personaje
        Guardado.Personaje.Posicion = Personaje.transform.position;
        Guardado.Personaje.Rotacion.y = Personaje.transform.localEulerAngles.y;
        Guardado.Personaje.Rotacion.x = Personaje.Camara.RotacionX;

        //Guardamos el nombre
        Guardado.Personaje.Perfil.Nombre = Personaje.name;

        //Guardamos las estadisticas
        Guardado.Personaje.Estadisticas = Personaje.Estadisticas.Estadisticas;

        //Guardamos inventario
        Guardado.Personaje.Inventario = Personaje.Inventario.Inventario;

        //Ruta de guardado
        string ruta = Directory.GetCurrentDirectory() + "/Guardadito.Savecito"; 

        //Escribimos el archivo en formato Json
        File.WriteAllText(ruta,JsonUtility.ToJson(Guardado,true));
    }

}
