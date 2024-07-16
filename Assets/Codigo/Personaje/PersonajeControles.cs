using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeControles : MonoBehaviour
{
    public float RatonX, RatonY;
    PersonajeMovimiento Movimiento;
    PersonajeRayos Rayos;
    private void Awake()
    {
        Movimiento=GetComponent<PersonajeMovimiento>();
        Rayos= GetComponent<PersonajeRayos>();
    }
    void Update()
    {
        Controles();
    }

    public void Controles()
    {
        //Los Axis, son ejes de unity, en la configuracion se pueden ver y se deben llamar IGUAL
        Movimiento.Ejes.x = Input.GetAxisRaw("Horizontal");
        Movimiento.Ejes.z = Input.GetAxisRaw("Vertical");
        RatonY = Input.GetAxis("Mouse Y");
        RatonX = Input.GetAxis("Mouse X");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //LLamar a saltar
            Movimiento.Saltar();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //empiezo a correr
            Movimiento.Correr(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //Dejo de correr
            Movimiento.Correr(false);
        }
        if(Input.GetMouseButtonDown(0))
        {
            Rayos.Interaccion();
        }
    }
}
