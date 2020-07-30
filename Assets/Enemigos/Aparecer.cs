using UnityEngine;

public class Aparecer : MonoBehaviour
{


    public bool dibujarGizmos = false;
    public LugarAparecer lugarAparecer = LugarAparecer.derecha;
    public float limitarLugarAparecerMinimo = 1.0f;
    public float limitarLugarAparecerMaximo = -1.0f;
    public bool velocidadRelativaAlJugador = false;
    public bool movimientoRelativoAlJugador = false;
    public bool OrientacionHaciaEnemigo = true;

    private Vector2 linearVelocity = new Vector2();
    private Rigidbody2D rigidBody = new Rigidbody2D();
    private GameObject jugadorObjetivo;
    private Enemigo enemigo;
    private GameManager manager;
    private Vector2 posicionInicial;
   


    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        jugadorObjetivo = GameObject.Find("Jugador");
        enemigo = GetComponent<Enemigo>();
        rigidBody = GetComponent<Rigidbody2D>();
        if (!enemigo) enemigo = GetComponentInChildren<Enemigo>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();        
        posicionInicial = transform.position;
    }


    // Use this for initialization
    private void Start()
    {
        PosicionarEnemigoEnInicio();
        AsignarVelocidadMovimiento();
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
                AsignarVelocidadMovimiento();
            }
        }
    }



    //----------------------METODOS---------------------------------------------


    /// <summary>
    /// Pone el enemigo en una posición inicial fuera de la pantalla
    /// </summary>
    private void PosicionarEnemigoEnInicio()
    {
        if (enemigo.inicioAleatorio)
        {
            #region pone al enemigo en una posicion al azar validando en que lugar puede aparecer (arriba, derecha, abajo, izquierda o varias) 
            LugarAparecer dondeAparecer = enemigo.lugarAparecerAleatorio;
            if (enemigo.lugarAparecerAleatorio == LugarAparecer.vertical)
            {
                int aux = Random.Range(1, 3);
                if (aux == 2)
                    dondeAparecer = LugarAparecer.abajo;
                else
                    dondeAparecer = LugarAparecer.arriba;
            }

            if (enemigo.lugarAparecerAleatorio == LugarAparecer.horizontal)
            {
                int aux = Random.Range(1, 3);
                if (aux == 2)
                    dondeAparecer = LugarAparecer.izquierda;
                else
                    dondeAparecer = LugarAparecer.derecha;
            }

            if (enemigo.lugarAparecerAleatorio == LugarAparecer.frente)
            {
                int aux = Random.Range(1, 4);
                if (aux == 1)
                    dondeAparecer = LugarAparecer.arriba;
                if (aux == 2)
                    dondeAparecer = LugarAparecer.derecha;
                if (aux == 3)
                    dondeAparecer = LugarAparecer.abajo;
            }

            if (enemigo.lugarAparecerAleatorio == LugarAparecer.todo)
            {
                int aux = Random.Range(1, 5);
                if (aux == 1)
                    dondeAparecer = LugarAparecer.arriba;
                if (aux == 2)
                    dondeAparecer = LugarAparecer.derecha;
                if (aux == 3)
                    dondeAparecer = LugarAparecer.abajo;
                if (aux == 4)
                    dondeAparecer = LugarAparecer.izquierda;
            }

            Vector2 posicionRandom = new Vector2();
            if (dondeAparecer == LugarAparecer.arriba)
            {
                posicionRandom = new Vector2(Random.Range(manager.extremoIzquierda.x, manager.extremoDerecha.x), manager.extremoArriba.y + 1 );
            }
            if (dondeAparecer == LugarAparecer.derecha)
            {
                posicionRandom = new Vector2(manager.extremoDerecha.x + 1, Random.Range(manager.extremoAbajo.y + limitarLugarAparecerMinimo, manager.extremoArriba.y + limitarLugarAparecerMaximo));
            }
            if (dondeAparecer == LugarAparecer.abajo)
            {
                posicionRandom = new Vector2(Random.Range(manager.extremoIzquierda.x, manager.extremoDerecha.x), manager.extremoAbajo.y - 1);
            }
            if (dondeAparecer == LugarAparecer.izquierda)
            {
                posicionRandom = new Vector2(manager.extremoIzquierda.x - 1, Random.Range(manager.extremoAbajo.y, manager.extremoArriba.y));
            }
            transform.position = posicionRandom;
            #endregion
        }
        else
        {
            transform.position = posicionInicial;           
        }    
    }

    private void AsignarVelocidadMovimiento()
    {


        if (movimientoRelativoAlJugador)
        {
            //transform.LookAt(jugadorObjetivo.transform.position);
            if (velocidadRelativaAlJugador)
            {
                //Inicia con una velocidad más rapida mientras mas lejos aparezca del jugador
                linearVelocity = (jugadorObjetivo.transform.position - transform.position);
                rigidBody.velocity = linearVelocity * (enemigo.velocidad / 10);
            }
            else
            {
                linearVelocity = (jugadorObjetivo.transform.position - transform.position);
                linearVelocity.Normalize();
                rigidBody.velocity = linearVelocity * enemigo.velocidad;
            }
        }
        else
        {
            if (enemigo.orientacionHorizontal == OrientacionHorizontal.izquierda)
            {
                linearVelocity = -transform.right;
                rigidBody.velocity = linearVelocity * enemigo.velocidad;
            }
            else
            {
                linearVelocity = transform.right;
                rigidBody.velocity = linearVelocity * enemigo.velocidad;
            }

        }

        var dir = jugadorObjetivo.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 180f; //punta del sprite en lado izquierdo
        transform.rotation = new Quaternion();
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));


        //determinar si apunta su cara deracha o izquierda viendo al jugador y cambiarla si aplica
        if (jugadorObjetivo.transform.position.x > transform.position.x)
        {
            transform.Rotate(new Vector3(1, 0, 0), 180f);
            //    // transform.Rotate(new Vector3(0, 180, 0));
               enemigo.orientacionHorizontal = OrientacionHorizontal.derecha;
        }
        else
        {
            transform.Rotate(new Vector3(1, 0, 0), 0f);
            enemigo.orientacionHorizontal = OrientacionHorizontal.izquierda;
        }




        }


    public void AsignarVelocidad(float val = 0)
    {
        enemigo.velocidad = val;
        rigidBody.velocity = linearVelocity * enemigo.velocidad;
    }

}
