using UnityEngine;

//Esto añadira y forzara estos componentes en el objeto al que meta este script
[RequireComponent(typeof(PersonajeCamara),typeof(PersonajeColisiones),typeof(PersonajeGravedad))]
[RequireComponent(typeof(PersonajeEstadisticas), typeof(PersonajeControles), typeof(PersonajeInterfaz))]
[RequireComponent(typeof(PersonajeInventario), typeof(PersonajeMovimiento), typeof(PersonajeRayos))]
public class PersonajeSistemas : MonoBehaviour
{
    public PersonajeCamara Camara;
    public PersonajeColisiones Colisiones;
    public PersonajeGravedad Gravedad;
    public PersonajeEstadisticas Estadisticas;
    public PersonajeControles Controles;
    public PersonajeInterfaz Interfaz;
    public PersonajeInventario Inventario;
    public PersonajeMovimiento Movimiento;
    public PersonajeRayos Rayos;

    private void Awake()
    {
        Camara=GetComponent<PersonajeCamara>();
        Colisiones=GetComponent<PersonajeColisiones>();
        Gravedad=GetComponent<PersonajeGravedad>();
        Estadisticas=GetComponent<PersonajeEstadisticas>();
        Controles=GetComponent<PersonajeControles>();
        Interfaz=GetComponent<PersonajeInterfaz>();
        Inventario=GetComponent<PersonajeInventario>();
        Movimiento=GetComponent<PersonajeMovimiento>();
        Rayos=GetComponent<PersonajeRayos>();
        //FindAnyObjectByType<GestorGuardado>();
        FindFirstObjectByType<GestorGuardado>().Personaje = this;
        //FindFirstObjectByType(typeof(GestorGuardado));
    }
}
