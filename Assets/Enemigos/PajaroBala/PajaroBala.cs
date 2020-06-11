using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PajaroBala : Enemigo
{



    public float velocidad = 3f;
    public float velocidadGiro = 200f;

    private Rigidbody2D rigidBody;
    private GameObject objetivo;
    private GameManager manager;


    // Use this for initialization
    new void Start()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        objetivo = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PosicionarEnemigoEnInicio();
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        if (objetivo)
        {
            Vector2 direccion = (Vector2)objetivo.transform.position - (Vector2)transform.position;
            direccion.Normalize();
            float anguloGirar = Vector3.Cross(direccion, -transform.right).z;
            rigidBody.angularVelocity = -anguloGirar * velocidadGiro;
            rigidBody.velocity = -transform.right * velocidad;
        }

    }


    // Este tipo de enemigo siempre muere y explota cuando colisiona con jugador
    new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Jugador jugador = collision.GetComponent<Jugador>();
        if (jugador)
        {
            if (!jugador.invencible)
                base.MuereEnemigo();
        }

    }




    /// <summary>
    /// Pone el enemigo en posicion inicial randon de la pantalla
    /// </summary>
    void PosicionarEnemigoEnInicio()
    {
        Vector2 posicionInicial = new Vector2(manager.extremoDerecha.x + 2, Random.Range(manager.extremoAbajo.y, manager.extremoArriba.y));
        transform.position = posicionInicial;
    }




}
