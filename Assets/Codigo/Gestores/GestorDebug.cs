using TMPro;
using UnityEngine;

public class GestorDebug : MonoBehaviour
{
    public TMP_Text FPSTexto;
    public float FPS;
    public delegate void Accion(bool opcion);
    public static event Accion ModoDebug;
    public bool Debug;
    private float fpsAcumulados;
    private float tiempoAcumulado;
    
    void Start()
    {
        if(ModoDebug != null)
        {
            ModoDebug(Debug);
            FPSTexto.gameObject.SetActive(Debug);
        }
    }

  
    void Update()
    {
        ContadorFPS();
    }

    public void ContadorFPS()
    {
        tiempoAcumulado += Time.deltaTime;
        fpsAcumulados+= 1;
        if(tiempoAcumulado>=0.5f)
        {
            FPS = fpsAcumulados / tiempoAcumulado;
            tiempoAcumulado = 0;
            fpsAcumulados = 0;
            FPSTexto.text = "FPS: "+(int)FPS;
        }
       
    }
}
