using UnityEngine;

public class PersonajeInventario : MonoBehaviour, ITieneMonedas
{
    PersonajeSistemas Personaje;
    public Inventario Inventario;

    private void Awake()
    {
        Personaje = GetComponent<PersonajeSistemas>();
    }
    private void Start()
    {
        ModificarMonedas(0);
    }
    public void ModificarMonedas(int cantidad)
    {
        Inventario.ModificarMonedas(cantidad);
        Personaje.Interfaz.ActualizarTextoMonedas(Inventario);
    }
}
