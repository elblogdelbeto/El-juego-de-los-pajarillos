using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponentInParent<Enemigo>() || collision.GetComponentInChildren<Enemigo>())
        {
            Enemigo enem = collision.GetComponentInChildren<Enemigo>();
            if (!enem)
            {
                enem = collision.GetComponentInParent<Enemigo>();
            }
            enem.MuereEnemigo(true);

        }

        else if (collision.GetComponent<Disparo>())
        {
            Destroy(collision.gameObject);
        }

    }




}
