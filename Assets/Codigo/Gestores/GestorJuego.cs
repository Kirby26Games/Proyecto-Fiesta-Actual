using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorJuego : MonoBehaviour
{
    public static async Task CambiarEscenaAsincrona(int ID)
    {
        AsyncOperation OperacionAsincrona;
        OperacionAsincrona = SceneManager.LoadSceneAsync(ID);
        while(!OperacionAsincrona.isDone)
        {
            print(OperacionAsincrona.progress);
            await Task.Yield();
        }
    }
    public static void CambiarEscena(int ID)
    {
        SceneManager.LoadScene(ID);
    }
    public static void Salir()
    {
        Application.Quit();
        print("Me voy");
    }
}
