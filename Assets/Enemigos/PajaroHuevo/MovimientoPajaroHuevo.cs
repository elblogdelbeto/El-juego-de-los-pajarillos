using UnityEngine;

public class MovimientoPajaroHuevo : MonoBehaviour
{


    public float velocidad = 1f;
    public bool dibujarGizmos = false;

    private Enemigo enemigo;
    private GameManager manager;
    private Rigidbody2D rigidBody;
    private GameObject objetivo;
    private Vector2 posicionInicial;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enemigo = GetComponent<Enemigo>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        objetivo = GameObject.FindGameObjectWithTag("Player");
        posicionInicial = transform.position;
        velocidad = Random.Range(1f, 1f) * velocidad;
    }


    // Use this for initialization
    private void Start()
    {
        PosicionarEnemigoEnInicio();

    }


    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        if (enemigo.reaparecer)
        {
            if (
                transform.position.x < manager.extremoIzquierda.x - 3 ||
                transform.position.x > manager.extremoDerecha.x + 3 ||
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
        if (enemigo.inicioAleatorio)
        {
            Vector2 posicionRandom = new Vector2();
            if(enemigo.lugarAparecerAleatorio == LugarAparecer.vertical)
            {
                int aux = Random.Range(1, 3);
                if (aux == 2)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.abajo;
                else
                    enemigo.lugarAparecerAleatorio = LugarAparecer.arriba;
            }

            if (enemigo.lugarAparecerAleatorio == LugarAparecer.horizontal)
            {
                int aux = Random.Range(1, 3);
                if (aux == 2)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.izquierda;
                else
                    enemigo.lugarAparecerAleatorio = LugarAparecer.derecha;
            }

            if (enemigo.lugarAparecerAleatorio == LugarAparecer.frente)
            {
                int aux = Random.Range(1, 4);
                if (aux == 1)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.arriba;
                if (aux == 2)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.derecha;
                if (aux == 3)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.abajo;
            }

            if (enemigo.lugarAparecerAleatorio == LugarAparecer.todo)
            {
                int aux = Random.Range(1, 5);
                if (aux == 1)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.arriba;
                if (aux == 2)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.derecha;
                if (aux == 3)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.abajo;
                if (aux == 4)
                    enemigo.lugarAparecerAleatorio = LugarAparecer.izquierda;
            }

            if (enemigo.lugarAparecerAleatorio == LugarAparecer.arriba)
            {
                posicionRandom = new Vector2(Random.Range(manager.extremoIzquierda.x, manager.extremoDerecha.x), manager.extremoArriba.y + 1 );
            }
            if (enemigo.lugarAparecerAleatorio == LugarAparecer.derecha)
            {
                posicionRandom = new Vector2(manager.extremoDerecha.x + 1, Random.Range(manager.extremoAbajo.y, manager.extremoArriba.y));
            }
            if (enemigo.lugarAparecerAleatorio == LugarAparecer.abajo)
            {
                posicionRandom = new Vector2(Random.Range(manager.extremoIzquierda.x, manager.extremoDerecha.x), manager.extremoAbajo.y - 1);
            }
            if (enemigo.lugarAparecerAleatorio == LugarAparecer.izquierda)
            {
                posicionRandom = new Vector2(manager.extremoIzquierda.x - 1, Random.Range(manager.extremoAbajo.y, manager.extremoArriba.y));
            }
            transform.position = posicionRandom;           
        }
        else
        {
            transform.position = posicionInicial;           
        }

        Vector2 movimiento = (objetivo.transform.position - transform.position);
        rigidBody.velocity = movimiento * velocidad;

        if (objetivo.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }




}
