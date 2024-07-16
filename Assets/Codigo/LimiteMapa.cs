using UnityEngine;

public class LimiteMapa : MonoBehaviour,IColisionable
{
    public int Da�oAlTocar;
    public bool CambioEscena, Reinicio;
    public int IDEscena;

    public void AlColisionar(GameObject Objeto)
    {
        if (CambioEscena)
        {
            GestorJuego.CambiarEscenaAsincrona(IDEscena);
        }
        if (Reinicio)
        {
            GestorAparicion.Instancia.Reaparecer();
        }
        if (Da�oAlTocar != 0)
        {
            ITieneVida ObjetoADa�ar =  Objeto.GetComponent<ITieneVida>();
            if(ObjetoADa�ar != null)
            {
                ObjetoADa�ar.ModificarVidaPerdida(Da�oAlTocar);
            }
        }
    }
}
