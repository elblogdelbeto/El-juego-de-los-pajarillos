using UnityEngine;

public class Aparecer : MonoBehaviour
{

    private GameManager manager;

    // Use this for initialization
    void Start()
    {

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PosicionarEnemigoEnInicio();

    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// Pone el enemigo en posicion inicial randon de la pantalla
    /// </summary>
    void PosicionarEnemigoEnInicio()
    {
        Vector2 posicionInicial = new Vector2(manager.extremoDerecha.x + 2, Random.Range(manager.extremoArriba.y - 5, manager.extremoArriba.y - 1));
        transform.position = posicionInicial;
    }
}
