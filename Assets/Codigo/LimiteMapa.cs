using UnityEngine;

public class LimiteMapa : MonoBehaviour,IColisionable
{
    public int DañoAlTocar;
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
        if (DañoAlTocar != 0)
        {
            ITieneVida ObjetoADañar =  Objeto.GetComponent<ITieneVida>();
            if(ObjetoADañar != null)
            {
                ObjetoADañar.ModificarVidaPerdida(DañoAlTocar);
            }
        }
    }
}
