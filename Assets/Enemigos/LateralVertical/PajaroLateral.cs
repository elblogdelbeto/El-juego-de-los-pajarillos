using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PajaroLateral : MonoBehaviour
{
    
    public float velocidad = 5f;
    public bool dibujarGizmos = false;

    private GameManager manager;
    private Rigidbody2D rigidBody;
    private Animator animator;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = transform.gameObject.GetComponentInChildren<Animator>();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Vector2 movimiento = new Vector2(-1, 0);
        rigidBody.velocity = movimiento * velocidad;
    }
    

    // Use this for initialization
    private void Start()
    {              
        PosicionarEnemigoEnInicio();  

        AsignarVelocidadMov();
    }


    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        if(transform.position.x < manager.extremoIzquirda.x-3)
        {
            PosicionarEnemigoEnInicio();
        }
    }


    //----------------------METODOS-------------------------------------------



    /// <summary>
    /// Pone el enemigo en posicion inicial random de la pantalla a la derecha afuera
    /// </summary>
    void PosicionarEnemigoEnInicio()
    {
        Vector2 posicionInicial = new Vector2(manager.extremoDerecha.x + 2, Random.Range(manager.extremoAbajo.y, manager.extremoArriba.y));
        transform.position = posicionInicial;
    }

    public void AsignarVelocidadMov()
    {
        if(animator)
            animator.SetFloat("VelocidadMov", Random.Range(0.5f, 2.0f));
    }

}
