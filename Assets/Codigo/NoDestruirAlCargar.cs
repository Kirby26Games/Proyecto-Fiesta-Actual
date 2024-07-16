using UnityEngine;

public class NoDestruirAlCargar : MonoBehaviour
{
    private void Awake()
    {
        //Hace que el objeto no se destruya entre escenas
        DontDestroyOnLoad(gameObject);
    }
}
