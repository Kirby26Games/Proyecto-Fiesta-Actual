using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeColisiones : MonoBehaviour
{
    //
    //Hay dos tipos de colisiones, Trigger(ColisionBlanda) y Collision(ColisionDura)

    //Debe tener IsTrigger activado en la colision
    private void OnTriggerEnter(Collider objetoTocado)
    {
        IAEnemigo enemigo= objetoTocado.transform.root.GetComponent<IAEnemigo>();
        if(enemigo != null )
        {
            if (objetoTocado == enemigo.ColisionDeteccion)
            {
                //Le decimos que nos persiga
                enemigo.DefinirObjetivo(gameObject);
            }
            if(objetoTocado == enemigo.ColisionAtaque)
            {
                //Hago que me ataque, pasandole mi objeto.
                enemigo.Atacar(gameObject);
            }
        }
        IConsumible objetoConsumible = objetoTocado.GetComponent<IConsumible>();
        if (objetoConsumible != null)
        {
            objetoConsumible.AlConsumir(gameObject);
        }
        //si toco un objeto colisionable, ejecuto alcolisionar
        IColisionable objetoColisionable = objetoTocado.gameObject.GetComponent<IColisionable>();
        if (objetoColisionable != null)
        {
            objetoColisionable.AlColisionar(gameObject);
        }
    }
    private void OnTriggerExit(Collider objetoTocado)
    {
        
    }

    //Debe tener IsTrigger desactivado en la colision
    private void OnCollisionEnter(Collision objetoTocado)
    {
        //Si el objeto tiene el tag plataforma
        if(objetoTocado.transform.CompareTag("Plataforma"))
        {
        //Lo convierto en mi padre
           transform.parent = objetoTocado.transform;
        }

        //si toco un objeto colisionable, ejecuto alcolisionar
        IColisionable objetoColisionable = objetoTocado.gameObject.GetComponent<IColisionable>();
        if(objetoColisionable != null )
        {
            objetoColisionable.AlColisionar(gameObject);
        }
    }
    private void OnCollisionExit(Collision objetoTocado)
    {
        //Si el objeto tiene el tag plataforma
        if (objetoTocado.transform.CompareTag("Plataforma"))
        {
            //Lo convierto en mi padre
            transform.parent = null;
        }
    }
}
