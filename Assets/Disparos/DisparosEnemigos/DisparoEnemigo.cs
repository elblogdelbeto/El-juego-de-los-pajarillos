using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour {

    public AudioClip sonidoDisparo;
    public GameObject disparoPrefab;
    public float disparoVelocidad = 6f;
    public float disparosPorSegundo = 0.5f;

    //
    // EVENTOS
    //

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float probabilidad = Time.deltaTime * disparosPorSegundo;
       
        if (Random.value <= probabilidad)
        {
            Disparar();
        }       

    }



    //
    //  METODOS
    //

    void Disparar()
    {
        AudioSource.PlayClipAtPoint(sonidoDisparo, transform.position);
        Vector2 inicioDisparo = new Vector2(transform.position.x, transform.position.y);
        GameObject disparo = Instantiate(disparoPrefab, inicioDisparo, Quaternion.identity);
        disparo.GetComponent<Rigidbody2D>().velocity = new Vector2(-disparoVelocidad, 0);
    }
}
