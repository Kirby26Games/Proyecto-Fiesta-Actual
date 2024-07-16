using System.Threading.Tasks;
using UnityEngine;

public class CreaCubos : MonoBehaviour
{
   //Hacer referencia al cubo que vamos a crear
    public GameObject CuboFiesta;
    //Definir los limites de la cuadricula en la que los vamos a crear,X Y y Z
    [Range(1, 1000)]
    public int LimiteX, LimiteY, LimiteZ;
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        CrearCubos();   
    }

 async void CrearCubos()
    {
        for (int x = 0; x < LimiteX; x++)
        {
            for (int y = 0; y < LimiteY; y++)
            {
                for (int z = 0; z < LimiteZ; z++)
                {
                    GameObject CuboNuevo = Instantiate(CuboFiesta);
                    CuboNuevo.transform.parent = transform;
                    CuboNuevo.transform.localPosition = new Vector3(x, y, z);
                }
                    await Task.Delay(1);
            }
        }
    }
}
