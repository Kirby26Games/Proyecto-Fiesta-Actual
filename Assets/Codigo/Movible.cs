using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Movible : MonoBehaviour
{
    private Rigidbody _RigidBody;
    public float ModificadorTraccion;
    public float FuerzaDeTraccionMaxima;
        private float VelocidadTraccion;
    private float Distancia;
    private void Awake()
    {
        _RigidBody = GetComponent<Rigidbody> ();
    }

    public void MoverHacia(Transform objetivo)
    {
        Distancia = Vector3.Distance(objetivo.position,transform.position);
        VelocidadTraccion += Time.deltaTime/Distancia* ModificadorTraccion;
        VelocidadTraccion= Mathf.Clamp(VelocidadTraccion,0,FuerzaDeTraccionMaxima);
        Vector3 velocidadTemporal = (objetivo.position - transform.position) *VelocidadTraccion;

        //Acciones segun distancia
        
        if(Distancia>5f)
        {
            velocidadTemporal.y = _RigidBody.linearVelocity.y;
        }
        if(Distancia<=5)
        {
        _RigidBody.useGravity = false;

        }
        if(Distancia<=2)
        {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0),1000*Time.deltaTime);
        _RigidBody.angularVelocity = Vector3.zero;

        }
        _RigidBody.linearVelocity = velocidadTemporal;
    }

    public void Liberar()
    {
        VelocidadTraccion = 0;
        _RigidBody.useGravity = true;
    }
}
