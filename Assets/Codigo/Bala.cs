using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidad=2;
    public int Daño=2;
    float temporizador;
    Vector3 Origen;
    public Torreta Dueño;
    public Collider Colision;
    public float TiempoDeVidaMaximo = 10;

    private void Awake()
    {
        Colision = GetComponent<Collider>();
    }
    private void Start()
    {
        Origen = transform.position;
    }
  
    void Update()
    {
        SistemaTiempo();
        Movimiento();
    }

    void SistemaTiempo()
    {
        temporizador += Time.deltaTime;
        if (temporizador >= TiempoDeVidaMaximo)
        {
            LimpiarBala();
        }
    }
    void Movimiento()
    {
        transform.position += transform.forward * Velocidad * Time.deltaTime;
    }
    public async void LimpiarBala()
    {
        Dueño.LimpiarDeLista(Colision);
        await GestorBasura.EliminarEnTiempo(gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Cuando colisione con un objeto que tenga vida
        //Le hace daño
        //Desaparece
        ITieneVida Objeto = collision.gameObject.GetComponent<ITieneVida>();
        if (Objeto != null)
        {
            Objeto.ModificarVidaPerdida(Daño);
        }
        PersonajeSistemas Personaje = collision.gameObject.GetComponent<PersonajeSistemas>();

        if (Personaje == null)
        {

            GestorDecals.Instancia.CrearDecal(Origen, collision.GetContact(0).point,collision.gameObject);
        }

        LimpiarBala();
        //Probad quitando el kinematic a la bala y activando trigger en el cañon si no os funciona
    }
}
