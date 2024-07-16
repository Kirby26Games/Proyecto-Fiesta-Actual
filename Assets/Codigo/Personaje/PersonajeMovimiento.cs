using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    [Header("Referencias")]
    PersonajeGravedad _Gravedad;
    PersonajeRayos _Rayos;
    Rigidbody _RigidBody;
    [Header("Movimiento")]
    public Vector3 Ejes;
    public Vector3 DireccionXZ;
    public Vector3 DireccionFinal;
    public float VelocidadBase;
    public float MultiplicadorAlCorrer;
    float VelocidadFinal;
    float VelocidadModificador=1;
    [Header("Salto")]
    public float DistanciaSalto=2;
    public int SaltosEnElAireMaximos;
    public int SaltosEnElAireActuales;

    private void Awake()
    {
        _Gravedad = GetComponent<PersonajeGravedad>();
        _RigidBody = GetComponent<Rigidbody>();
        _Rayos = GetComponent<PersonajeRayos>();
    }

    void Start()
    {
        CalcularVelocidad();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        ReiniciarSaltos();
    }

    void Movimiento()
    {
        DireccionXZ = new Vector3(Ejes.x,0,Ejes.z).normalized;
        DireccionFinal= transform.TransformDirection(DireccionXZ)*VelocidadFinal;
        DireccionFinal = Vector3.ProjectOnPlane(DireccionFinal, _Rayos.DatosPendiente.normal);
        DireccionFinal.y += Ejes.y;
        _RigidBody.linearVelocity = DireccionFinal;

    }

    public void Correr(bool corriendo)
    {
        if (corriendo)
        {
            VelocidadModificador = MultiplicadorAlCorrer;
        }
        else
        {
            VelocidadModificador = 1;
        }
        CalcularVelocidad();
    }
    public void CalcularVelocidad()
    {
        VelocidadFinal = VelocidadBase * VelocidadModificador;
    }
    public void Saltar()
    {
        if(!PuedoSaltar())
        {
            return;
        }
        //Salto
        Ejes.y = Mathf.Sqrt(DistanciaSalto*-2*_Gravedad.Gravedad);
    }
    public bool PuedoSaltar()
    {
        bool puedo = false;
        //Si estoy en el suelo, siempre puedo saltar
        if(_Rayos.EnSuelo)
        {
            puedo = true; 
        }
        //si estoy en el aire, puedo saltar si no he llegado a los saltos maximos
        else if(SaltosEnElAireActuales<SaltosEnElAireMaximos)
        {
            puedo = true;
            SaltosEnElAireActuales++;
        }
        return puedo;
    }

    void ReiniciarSaltos()
    {
        if(_Rayos.EnSuelo)
        {
            SaltosEnElAireActuales = 0;
        }
    }
}
