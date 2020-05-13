using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TucanVerdeMov : MonoBehaviour {

    
    private GameManager manager;
   
    // Awake is called when the script instance is being loaded
    private void Awake()
    {

    }


    // Use this for initialization
    private void Start()
    {      

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PosicionarEnemigoEnInicio();              
     
    }

        
    //----------------------METODOS-------------------------------------------



    /// <summary>
    /// Pone el enemigo en posicion inicial random de la pantalla
    /// </summary>
    void PosicionarEnemigoEnInicio()
    {
        Vector2 posicionInicial = new Vector2(Random.Range((manager.extremoIzquirda.x + manager.extremoDerecha.x) / 2 + 2,manager.extremoDerecha.x - 1), Random.Range(manager.extremoAbajo.y, manager.extremoArriba.y));
        transform.position = posicionInicial;
    }


}
