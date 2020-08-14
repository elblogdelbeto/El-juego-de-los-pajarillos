using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


enum TipoControles
{
    Tactiles,
    BotonesFisicos
}

[RequireComponent(typeof(Jugador))]
public class TouchAndControls : MonoBehaviour
{

    public float velocidadStick = 3F;
    public float velocidadMovimiento = 20F;
    public float velocidadSeguimiento = 10F;   

    GameManager gameManager;
    Rigidbody2D rb2D;
    Jugador jugador;
    Vector3 touchedPos;
    Vector2 inicioVec = new Vector2();
    Vector2 finVec = new Vector2();
    Touch touch;
    Vector2 pastPosition;
    public GameObject botonItem { get; set; }
    float anguloZ = 0;
    float anguloY = 0f;
    float ButtonCooler = 0.2f;
    int ButtonCount = 0;
    bool touchBeganFueraboton = false;
    int cantidadTouches = 0;
    int maxResolucion = 0;

    // EVENTOS ---------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        maxResolucion = Math.Max(Screen.width, Screen.height);
        jugador = gameObject.GetComponent<Jugador>();        
        rb2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (!jugador.puedeControlarse)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        }

        if (!MenuPausa.JuegoPausado && jugador.puedeControlarse)
        {
            if (Input.touchSupported
#if UNITY_EDITOR
                || UnityEditor.EditorApplication.isRemoteConnected
#endif
              )
            {
                Disparar(TipoControles.Tactiles);
                // MovimientoSeguirTouch();
                MovimientoTouchDeslizar();
            }
            else
            {              
                GirarEsquivar(false);                
                Voltearse(false);
                Disparar(TipoControles.BotonesFisicos);
                MoverteControlesVelocidad();
            }
        }
    }



    void FixedUpdate()
    {
        if (!MenuPausa.JuegoPausado && jugador.puedeControlarse)
        {
            if (Input.touchSupported
#if UNITY_EDITOR
                || UnityEditor.EditorApplication.isRemoteConnected
#endif
              )
            {
                MovimientoTouchAplicandoFisica();
            }
            else
            {

            }
        }
    }


    // METODOS ---------------------------------------------------------------------------------------------------------------------


    void MovimientoTouchDeslizar()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (Input.touchCount != cantidadTouches)
            {
                inicioVec = touch.position;
                finVec = touch.position;
                cantidadTouches = Input.touchCount;
            }
            if (touch.phase == TouchPhase.Began)
            {
                inicioVec = touch.position;
                finVec = touch.position;
            }
            if ((touch.phase == TouchPhase.Stationary) || (touch.phase == TouchPhase.Moved))
            {
                finVec = touch.position;

                if (touch.phase == TouchPhase.Moved)
                {
                    anguloZ = Mathf.Clamp(anguloZ + ((finVec.y - inicioVec.y) * 0.3f), -jugador.inclinacionMovimiento, jugador.inclinacionMovimiento);
                }

            }
            if (touch.phase == TouchPhase.Ended)
            {
                inicioVec = touch.position;
                finVec = touch.position;
            }
        }
        else
        {
            if (anguloZ > 2)
                anguloZ -= 70 * Time.deltaTime;
            else if (anguloZ < -2)
                anguloZ += 70 * Time.deltaTime;
            else
                anguloZ = 0;
        }
        anguloY = 0f;
        if (jugador.orientacion == OrientacionHorizontal.izquierda)
            anguloY = 180f;

        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, anguloY, anguloZ);
    }


    void MovimientoTouchAplicandoFisica()
    {
        rb2D.velocity = ((finVec - inicioVec) / maxResolucion) * velocidadMovimiento;
        inicioVec = touch.position;
        finVec = touch.position;
    }


    void MoverteControlesVelocidad()
    {
       

        float moverHorizontal = Input.GetAxis("Horizontal");
        float moverVertical = Input.GetAxis("Vertical");
        Vector2 movimiento = new Vector2(moverHorizontal, moverVertical);
        GetComponent<Rigidbody2D>().velocity = movimiento * velocidadStick;

        float anguloZ = 0;

        if (moverVertical > 0 || moverHorizontal < 0)
        {
            anguloZ = 0;
            if (moverVertical > Mathf.Abs(moverHorizontal))
                anguloZ = moverVertical * jugador.inclinacionMovimiento;
            else
                anguloZ = Mathf.Abs(moverHorizontal) * jugador.inclinacionMovimiento;
        }

        if (moverVertical < 0 || moverHorizontal > 0)
        {
            anguloZ = 0;
            if (moverHorizontal > Mathf.Abs(moverVertical))
                anguloZ = -moverHorizontal * jugador.inclinacionMovimiento;
            else
                anguloZ = moverVertical * jugador.inclinacionMovimiento;
        }

        float anguloY = 0f;
        if (jugador.orientacion == OrientacionHorizontal.izquierda)
            anguloY = 180f;

        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, anguloY, anguloZ);

    }


    void MoverteControlesPosicion()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position -= new Vector3(velocidad * Time.deltaTime, 0, 0);
            transform.position += Vector3.left * velocidadStick * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
            transform.position += new Vector3(velocidadStick * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += new Vector3(0, velocidadStick * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.position -= new Vector3(0, velocidadStick * Time.deltaTime, 0);

    }


    void MovimientoTouchDeslizar2()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero          
            if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved))
            {
                // Move object across XY plane
                Vector3 nuevaPosicion = new Vector3(touch.deltaPosition.x * velocidadMovimiento * Time.deltaTime, touch.deltaPosition.y * velocidadMovimiento * Time.deltaTime, 0);
                transform.Translate(nuevaPosicion);
            }
        }

    }


    void MovimientoSeguirTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved))
            {
                // get the touch position from the screen touch to world point
                touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x + 20, touch.position.y, 0));
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                transform.position = Vector3.Lerp(transform.position, touchedPos, velocidadSeguimiento * Time.deltaTime);

                anguloZ = Mathf.Clamp((touchedPos.y - transform.position.y) * 10, -jugador.inclinacionMovimiento, jugador.inclinacionMovimiento);
            }
        }
        else
        {
            if (anguloZ > 1)
                anguloZ -= Time.deltaTime * 20;
            else if (anguloZ < -1)
                anguloZ += Time.deltaTime * 20;
            else
                anguloZ = 0;
        }

        anguloY = 0f;
        if (jugador.orientacion == OrientacionHorizontal.izquierda)
            anguloY = 180f;

        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, anguloY, anguloZ);


    }



    /// <summary>
    /// se manda llamar del UI del boton del item
    /// </summary>
    public void UsarItem(bool botonTactil)
    {
        if (Input.GetButtonDown("UsarItem") || botonTactil)
        {
            switch (jugador.item.tipoItem)
            {
                case TipoItem.fuego:
                    Vector2 inicioDisparo = new Vector2(jugador.disparador.transform.position.x, jugador.disparador.transform.position.y);
                    Instantiate(jugador.item.objetoItemDisparo, inicioDisparo, jugador.disparador.transform.rotation, gameManager.contenedorDisparos.transform);
                    jugador.tiempoUltimoDisparo = Time.time;
                    botonItem.GetComponent<Button>().interactable = false;
                    botonItem.transform.GetChild(0).GetComponent<Image>().sprite = botonItem.transform.GetChild(0).GetComponent<ItemBoton>().spriteDefault;

                    break;
                case TipoItem.dinamita:
                    break;
                case TipoItem.iman:
                    break;
                case TipoItem.regalo:
                    break;
                case TipoItem.piedras:
                    break;
                default:
                    break;
            }
        }
    }

    public void GirarEsquivar(bool botonTouch) //tambien llamdo desde boton UI para mobil
    {
        if (Input.GetButtonDown("Esquivar") || botonTouch)
        {
            if (!jugador.invencible)
                jugador.animator.SetBool("GirarEsquivar", true);
        }
    }

    public void Voltearse(bool botonTouch)
    {
        // dos comandos para voltear
        if (Input.GetButtonDown("Voltear") || botonTouch)
        {
                if (jugador.orientacion == OrientacionHorizontal.derecha)
                {
                    transform.rotation = new Quaternion(0, 1, 0, 0);
                    jugador.orientacion = OrientacionHorizontal.izquierda;
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    jugador.orientacion = OrientacionHorizontal.derecha;
                }            
        }
        //VoltearseDobletap();        
    }



    private void VoltearseDobleTap()
    {
        //doble tap media vuelta
        if (Input.GetButtonDown("Horizontal"))
        {
            float moverHorizontal = Input.GetAxis("Horizontal");

            if (ButtonCooler > 0 && ButtonCount == 1)
            {
                if (moverHorizontal < 0)
                {
                    transform.rotation = new Quaternion(0, 1, 0, 0);
                    jugador.orientacion = OrientacionHorizontal.izquierda;
                }
                if (moverHorizontal > 0)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    jugador.orientacion = OrientacionHorizontal.derecha;
                }
            }
            else
            {
                ButtonCooler = 0.3f;
                ButtonCount += 1;
            }
        }
        if (ButtonCooler > 0)
            ButtonCooler -= 1 * Time.deltaTime;
        else
            ButtonCount = 0;
    }


    private void Disparar(TipoControles tipo)
    {
        if (jugador.puedeDisparar)
        {
            if (tipo == TipoControles.BotonesFisicos)
            {
                DispararBotones();
            }

            if (tipo == TipoControles.Tactiles)
            {
                DispararTactil();
            }
        }
    }

    private void DispararBotones()
    {

        Vector3 inicioDisparo = new Vector3(jugador.disparador.transform.position.x, jugador.disparador.transform.position.y, transform.position.z);
        Disparo disparo = jugador.disparoPrefab.GetComponent<Disparo>();

        if (Input.GetButtonDown("Fire1"))
        {

            if (!IsPointerOverUIObject())
            {
                if (Time.time >= jugador.tiempoUltimoDisparo + disparo.tiempoEntreDisparos * 0.5f)
                {
                    Instantiate(jugador.disparoPrefab, inicioDisparo, jugador.disparador.transform.rotation, gameManager.contenedorDisparos.transform);
                    gameManager.contenedorDisparos.disparosDesplegados++;
                    jugador.tiempoUltimoDisparo = Time.time;
                }
            }
        }
        //Disparo automatico
        if (Input.GetButton("Fire1") || jugador.disparoAutomatico)
        {
            if (!IsPointerOverUIObject())
            {
                if (Time.time >= jugador.tiempoUltimoDisparo + disparo.tiempoEntreDisparos)
                {
                    Instantiate(jugador.disparoPrefab, inicioDisparo, jugador.disparador.transform.rotation, gameManager.contenedorDisparos.transform);
                    gameManager.contenedorDisparos.disparosDesplegados++;
                    jugador.tiempoUltimoDisparo = Time.time;
                }
            }

        }


    }


    private void DispararTactil()
    {

        Vector3 inicioDisparo = new Vector3(jugador.disparador.transform.position.x, jugador.disparador.transform.position.y, transform.position.z);
        Disparo disparo = jugador.disparoPrefab.GetComponent<Disparo>();


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (touch.phase == TouchPhase.Began)
            {
                if (!IsPointerOverUIObject())
                {
                    touchBeganFueraboton = true;
                    if (Time.time >= jugador.tiempoUltimoDisparo + disparo.tiempoEntreDisparos * 0.5f)
                    {
                        Instantiate(jugador.disparoPrefab, inicioDisparo, jugador.disparador.transform.rotation, gameManager.contenedorDisparos.transform);
                        jugador.tiempoUltimoDisparo = Time.time;
                    }
                }
                else
                {
                    touchBeganFueraboton = false;
                }
            }
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved || jugador.disparoAutomatico)
            {
                if (Time.time >= jugador.tiempoUltimoDisparo + disparo.tiempoEntreDisparos)
                {
                    if (touchBeganFueraboton || !IsPointerOverUIObject())
                    {
                        Instantiate(jugador.disparoPrefab, inicioDisparo, jugador.disparador.transform.rotation, gameManager.contenedorDisparos.transform);
                        jugador.tiempoUltimoDisparo = Time.time;
                    }
                }
            }
        }

    }


    //When Touching UI
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
