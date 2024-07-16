using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeGravedad : MonoBehaviour
{
    PersonajeRayos Rayos;
    PersonajeMovimiento Movimiento;
    public float Gravedad = -9.82f;
    public float LimiteVelocidadCaida;


    private void Awake()
    {
        Movimiento = GetComponent<PersonajeMovimiento>();
        Rayos = GetComponent<PersonajeRayos>();
    }
    void Update()
    {
        CalcularGravedad();
    }
    public void CalcularGravedad()
    {

        if (Rayos.EnSuelo && Movimiento.Ejes.y <= 0)
        {
            Movimiento.Ejes.y = 0;
        }
        else if (Movimiento.Ejes.y > LimiteVelocidadCaida)
        {
            Movimiento.Ejes.y += Gravedad * Time.deltaTime;
        }
    }
}
