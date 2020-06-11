using UnityEngine;

public class PatoRedondoMov : MonoBehaviour
{


    public float velocidad = 0.5f;


    Vector3 startPos;
    Vector3 endPos;
    float trajectoryHeight = 4f;


    private GameManager manager;
    private Rigidbody2D rigidBody;
    private Vector2 movimiento = new Vector2(-1, 0);
    Jugador jugador;


    float tiempoLlegaObjetivo = 0f; //en 1 ya llegó

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //AsignarVelocidad(velocidad);
        jugador = GameObject.Find("Jugador").GetComponent<Jugador>();

    }




    // Use this for initialization
    private void Start()
    {
        PosicionarEnemigoEnInicio();
        //AsignarVelocidad(velocidad);       
    }


    // Update is called once per frame
    void Update()
    {


        // calculate current time within our lerping time range
        float deltaTiempo = Time.deltaTime * velocidad * Random.Range(0.8f, 1.2f);
        tiempoLlegaObjetivo = tiempoLlegaObjetivo + deltaTiempo;
        // calculate straight-line lerp position:
        Vector3 currentPos = Vector3.Lerp(startPos, endPos, tiempoLlegaObjetivo);
        // add a value to Y, using Sine to give a curved trajectory in the Y direction
        currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(tiempoLlegaObjetivo) * Mathf.PI);
        // finally assign the computed position to our gameObject:
        transform.position = currentPos;

        //if (transform.position.x < manager.extremoIzquirda.x - 3)
        if (transform.position == endPos)
        {
            tiempoLlegaObjetivo = 0;
            startPos = transform.position;
            AsignarObjetivo();
            if (jugador.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        //}

    }


    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        //if (transform.position.x < manager.extremoIzquirda.x - 3)
        //{
        //    PosicionarEnemigoEnInicio();
        //}
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

        Vector2 posicionInicial = new Vector2(manager.extremoDerecha.x + 1, Random.Range(manager.extremoAbajo.y - 3, manager.extremoArriba.y - 3));
        transform.position = posicionInicial;

        tiempoLlegaObjetivo = 0;
        startPos = transform.position;
        AsignarObjetivo();
    }

    public void AsignarObjetivo()
    {
        Vector2 nuevoObjetivo = new Vector2(jugador.transform.position.x * Random.Range(0.8f, 1.2f), jugador.transform.position.y * Random.Range(0.8f, 1.2f));
        endPos = nuevoObjetivo;
    }

}
