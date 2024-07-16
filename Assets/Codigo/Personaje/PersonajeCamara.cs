using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeCamara : MonoBehaviour
{
    public PersonajeControles Controles;
    public Camera Camara;
    public float RotacionX, RotacionY;
    public float Sensibilidad = 1;
    public float LimiteCamara;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//El cursor se pone en el centro
        Cursor.visible = false;//El cursor se hace invisible
    }

    void Update()
    {
        MovimientoCamara();
    }

    public void MovimientoCamara()
    {
        RotacionY = Controles.RatonX * Sensibilidad;
        RotacionX -= Controles.RatonY * Sensibilidad;

        //Limito el movimiento de la camara hacia arriba y hacia abajo
        //Mathf.Clamp coge un numero y hace que no pueda ser menor ni mayor que el limite que yo ponga
        RotacionX = Mathf.Clamp(RotacionX, -LimiteCamara, LimiteCamara);

        //Giro el personaje
        transform.Rotate(Vector3.up * RotacionY);


        //Giro la camara
        //transform.localEulerAngles = new Vector3(0, RotacionY, 0);

        Camara.transform.localEulerAngles = new Vector3(RotacionX, 0, 0);
    }
}
