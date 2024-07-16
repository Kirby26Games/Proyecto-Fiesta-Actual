using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidad=2;
    public int Da�o=2;
    float temporizador;
    Vector3 Origen;
    public Torreta Due�o;
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
        Due�o.LimpiarDeLista(Colision);
        await GestorBasura.EliminarEnTiempo(gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Cuando colisione con un objeto que tenga vida
        //Le hace da�o
        //Desaparece
        ITieneVida Objeto = collision.gameObject.GetComponent<ITieneVida>();
        if (Objeto != null)
        {
            Objeto.ModificarVidaPerdida(Da�o);
        }
        PersonajeSistemas Personaje = collision.gameObject.GetComponent<PersonajeSistemas>();

        if (Personaje == null)
        {

            GestorDecals.Instancia.CrearDecal(Origen, collision.GetContact(0).point,collision.gameObject);
        }

        LimpiarBala();
        //Probad quitando el kinematic a la bala y activando trigger en el ca�on si no os funciona
    }
}
