using UnityEngine;

public class Fondo : MonoBehaviour
{

    public float velocidadScroll = 5f;
    public float longitudFondo = 55;

    private Vector3 posicionInicial;

    // Use this for initialization
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float nuevaPosicion = Mathf.Repeat(Time.time * velocidadScroll, longitudFondo);
        transform.position = posicionInicial + Vector3.left * nuevaPosicion;
    }
}
