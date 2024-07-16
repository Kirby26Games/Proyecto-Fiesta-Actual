using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonajeEstadisticas : MonoBehaviour,ITieneVida
{
    private PersonajeSistemas Personaje;
    public Estadisticas Estadisticas;


    private void Awake()
    {
        Personaje=GetComponent<PersonajeSistemas>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ModificarVidaPerdida(0);
    }

    public void ModificarVidaPerdida(int cantidad)
    {
        Estadisticas.ModificarVidaPerdida(cantidad);
        if(Estadisticas.VidaActual==0)
        {
            //Si quiero reiniciar la escena
            //GestorJuego.CambiarEscena(SceneManager.GetActiveScene().buildIndex);

            //Si quiero volver al punto mas cercano y hacer cosas como quitar una vida, iria aqui
            GestorAparicion.Instancia.Reaparecer();
            Estadisticas.ModificarVidaPerdida(-Estadisticas.VidaMaxima);
            //Ejecuto el codigo que tenga de perder una vida en el juego
        }
        Personaje.Interfaz.ActualizarSliderVida(Estadisticas);
    }
}
