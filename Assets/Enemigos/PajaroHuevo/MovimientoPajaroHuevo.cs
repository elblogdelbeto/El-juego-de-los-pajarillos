using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPajaroHuevo : MonoBehaviour {


    public float velocidad = 1f;
    public bool dibujarGizmos = false;

    private Enemigo enemigo;
    private GameManager manager;
    private Rigidbody2D rigidBody; 
    private GameObject objetivo;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enemigo = GetComponent<Enemigo>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        objetivo = GameObject.FindGameObjectWithTag("Player");

        velocidad = Random.Range(1f, 1f) * velocidad;
    }


    // Use this for initialization
    private void Start()
    {
        if (enemigo.inicioAleatorio)
        {
            PosicionarEnemigoEnInicio();
        }
        else
        {
            Vector2 movimiento = (objetivo.transform.position - transform.position);
            rigidBody.velocity = movimiento * velocidad;
        }

        if(objetivo.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
       
    }


    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        if (enemigo.reaparecer)
        {
            if (
                transform.position.x < manager.extremoIzquirda.x - 3 ||
                transform.position.x > manager.extremoDerecha.x +3 ||
                transform.position.y < manager.extremoAbajo.y - 3 ||
                transform.position.y > manager.extremoArriba.y + 3 
                )
            {
                PosicionarEnemigoEnInicio();
            }
        }
    }


    //----------------------METODOS-------------------------------------------
    

    /// <summary>
    /// Pone el enemigo en posicion inicial random de la pantalla a la derecha afuera
    /// </summary>
    void PosicionarEnemigoEnInicio()
    {
        Vector2 posicionInicial = new Vector2(manager.extremoDerecha.x + 1, Random.Range(manager.extremoAbajo.y, manager.extremoArriba.y));
        transform.position = posicionInicial;

        Vector2 movimiento = (objetivo.transform.position - transform.position);
        rigidBody.velocity = movimiento * velocidad;

    }




}
