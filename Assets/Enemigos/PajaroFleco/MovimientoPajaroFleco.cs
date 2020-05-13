using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPajaroFleco : MonoBehaviour {

    public float velocidad = 5.0f;


    private GameManager manager;
    private Rigidbody2D rigidBody;
    private Vector2 movimiento = new Vector2(-1, 0);    

    // Awake is called when the script instance is being loaded
    private void Awake()
    {

    }


    // Use this for initialization
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PosicionarEnemigoEnInicio();

        AsignarVelocidad(velocidad);      
    }
    

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        if (transform.position.x < manager.extremoIzquirda.x - 3)
        {
            PosicionarEnemigoEnInicio();
        }
    }


    //----------------------METODOS-------------------------------------------

    public void AsignarVelocidad(float val)
    {
        velocidad = val;
        rigidBody.velocity = movimiento * velocidad;
    }



    /// <summary>
    /// Pone el enemigo en posicion inicial randon de la pantalla
    /// </summary>
    void PosicionarEnemigoEnInicio()
    {
        Vector2 posicionInicial = new Vector2(manager.extremoDerecha.x + 2, Random.Range(manager.extremoAbajo.y, manager.extremoArriba.y-2));
        transform.position = posicionInicial;
    }


}
