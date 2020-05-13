using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

    public GameObject enemigoPrefab;
    public float ancho = 4f;
    public float alto = 8f;
    public float velocidad = 4.0f;
    public float retrasoReclutar = 0.5f;

    private float xmin;
    private float xmax;
    private float margen = 0f;
    private bool avanzaDerecha = true;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {       
        float distanciaCamara = transform.position.z - Camera.main.transform.position.z;
        Vector3 extremoIzquierdo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanciaCamara));
        Vector3 extremoDerecho = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanciaCamara));
        xmin = extremoIzquierdo.x + margen + (ancho/2);
        xmax = extremoDerecho.x - margen - (ancho/2);

        ReclutarOleada();
    }
    

    // Use this for initialization
    void Start () {
       
    }

    // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(ancho, alto, 0));
    }
    
    // Update is called once per frame
    void Update () {

        if(avanzaDerecha)
        {
            transform.position += Vector3.right * velocidad * Time.deltaTime;
            foreach (Transform PosicionEnemiga in transform)
            {
                PosicionEnemiga.rotation = new Quaternion(0, 180, 0, 0);
            }            
            if(transform.position.x >= xmax)
            {
                avanzaDerecha = false;
            }
        }
        else
        {
            transform.position += Vector3.left * velocidad * Time.deltaTime;
            foreach (Transform PosicionEnemiga in transform)
            {
                PosicionEnemiga.rotation = Quaternion.identity;
            }                
            if (transform.position.x <= xmin)
            {
                avanzaDerecha = true;
            }
        }

        if(TodosEnemigosMuertos())
        {
            ReclutarHastaLlenar();
        }
       
    }

    void ReclutarOleada()
    {
        foreach (Transform item in transform)
        {
            GameObject enemigo = Instantiate(enemigoPrefab, item.position, Quaternion.identity);
            enemigo.transform.parent = item;
        }

    }

    void ReclutarHastaLlenar()
    {
        Transform frePosition = NextFreePosition();
        if (frePosition)
        {
            GameObject enemigo = Instantiate(enemigoPrefab, frePosition.position, Quaternion.identity);
            enemigo.transform.parent = frePosition;
        }
        if (NextFreePosition())
        {
            Invoke("ReclutarHastaLlenar", retrasoReclutar);
        }
        
    }

    Transform NextFreePosition()
    {
        foreach (Transform PosicionEnemigaGameObject in transform)
        {
            if (PosicionEnemigaGameObject.childCount == 0)
                return PosicionEnemigaGameObject;
        }
        return null;
    }

    bool TodosEnemigosMuertos()
    {
        foreach (Transform PosicionEnemigaGameObject in transform)
        {
            if (PosicionEnemigaGameObject.childCount > 0)
                return false;
        }
        return true;
    }
}
