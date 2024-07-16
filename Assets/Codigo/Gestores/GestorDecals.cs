using System.Collections.Generic;
using UnityEngine;

public class GestorDecals : MonoBehaviour
{
    public static GestorDecals Instancia;
    public GameObject Decal;
    public List<GameObject> Decals;
    public int DecalsMaximos=10;
    public int IDActual;

    private void Awake()
    {
        Instancia = this;
    }

    public void CrearDecal(Vector3 origen, Vector3 destino, GameObject objetoChocado)
    {
        RaycastHit[] Datos;

        Ray Rayo = new Ray(origen, destino - origen);
        float Distancia = Vector3.Distance(origen, destino);
        //RaycastAll da todos los objetos que chocan con el raycast desde el inicio hasta el final
        Datos = Physics.RaycastAll(Rayo.origin, Rayo.direction, Distancia + 0.1f);
        //Paso por los objetos con los que ha chocado el rayo.
        for (int i = 0; i < Datos.Length; i++)
        {
            //Si el objeto actual es el mismo que objetochocado creo el decal
            if (Datos[i].transform.gameObject == objetoChocado)
            {
                Vector3 Posicion = Datos[i].point + Datos[i].normal * 0.0001f;
                Quaternion Rotacion = Quaternion.FromToRotation(Vector3.back, Datos[i].normal);
                //Hago rayo de 10 segundos para probar
                Debug.DrawRay(Rayo.origin, Rayo.direction * Datos[i].distance, Color.white, 10f);
                if (DeboAñadirALista())
                {
                    GameObject NuevoDecal = Instantiate(Decal, Posicion, Rotacion, objetoChocado.transform);
                    Decals.Add(NuevoDecal);
                }
                else
                {
                    Decals[IDActual].transform.SetPositionAndRotation(Posicion, Rotacion);
                    Decals[IDActual].transform.parent = objetoChocado.transform;
                }
                IDActual++;
                if (IDActual >= DecalsMaximos)
                {
                    IDActual = 0;
                }
                break;
            }
        }
    }
    public bool DeboAñadirALista()
    {
        if (Decals.Count >= DecalsMaximos)
        {
            return false;
        }
        return true;
    }
}
