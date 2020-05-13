using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TucanVerdeEnemigo : Enemigo {


    public AudioClip sonidoDisparo;
    public GameObject disparoPrefab;
    public float disparoVelocidad = 6f;
    public float disparosPorSegundo = 0.5f;
    public int yaEnPosicion { get; set; } //se pone en 1 al final de animacion "Llegada" en evento Animator

 
    // Update is called once per frame
    void Update()
    {
        if (yaEnPosicion==1)
        {
            float probabilidad = Time.deltaTime * disparosPorSegundo;

            if (Random.value <= probabilidad)
            {
                Disparar();
            }
        }
      

    }

   

    void Disparar()
    {
        AudioSource.PlayClipAtPoint(sonidoDisparo, transform.position);
        Vector2 inicioDisparo = new Vector2(transform.position.x +0.4f, transform.position.y-0.4f);
        GameObject disparo = Instantiate(disparoPrefab, inicioDisparo, Quaternion.identity, contenedorDisparos.transform);
        disparo.GetComponent<Rigidbody2D>().velocity = new Vector2(-disparoVelocidad, 0);

        disparo = Instantiate(disparoPrefab, inicioDisparo, Quaternion.Euler(0,0,45/3), contenedorDisparos.transform);
        disparo.GetComponent<Rigidbody2D>().velocity = new Vector2(-disparoVelocidad, -(disparoVelocidad/3));

        disparo = Instantiate(disparoPrefab, inicioDisparo, Quaternion.Euler(0, 0, -(45/3)), contenedorDisparos.transform);
        disparo.GetComponent<Rigidbody2D>().velocity = new Vector2(-disparoVelocidad, (disparoVelocidad/3));
    }


}
