using UnityEngine;

public class OjosMovimiento : MonoBehaviour
{

    Jugador jugador;
    Vector3 posicionOjosInicial;

    private void Awake()
    {
        jugador = FindObjectOfType<Jugador>();
        posicionOjosInicial = transform.localPosition;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 difereniaJugadorConJefe = jugador.transform.position - transform.position;
        difereniaJugadorConJefe = new Vector3(Mathf.Clamp(difereniaJugadorConJefe.x / 60, -0.15f, 0.15f), Mathf.Clamp(difereniaJugadorConJefe.y / 40, -0.10f, 0.10f), difereniaJugadorConJefe.z);
        transform.localPosition = posicionOjosInicial + difereniaJugadorConJefe;

    }


}
