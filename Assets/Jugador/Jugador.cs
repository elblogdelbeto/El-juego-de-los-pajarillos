using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum OrientacionHorizontal
{
    izquierda,
    derecha
}


public class Jugador : MonoBehaviour {
    
    public Vector2 posicionInicial = new Vector2(-6, 0);
    public GameObject disparador;
    public GameObject disparoPrefab;
    public GameObject explosionPrefab;
    public GameObject danioPrefab;    
    public float saludInicial = 100f;
    public float damageChoque = 50f;
    public int vidas = 5;
    public bool disparoAutomatico = false;
    public AudioClip sonidoMuerte;
    public AudioClip sonidoDanio;
    public Item item;

    public int puedeDisparar { get; set; }
  

    [HideInInspector]
    public OrientacionHorizontal orientacion = OrientacionHorizontal.derecha;
    [HideInInspector]
    public float tiempoUltimoDisparo = 0;
    [HideInInspector]
    public float inclinacionMovimiento = 15f;
    [HideInInspector]
    public GameObject contenedorDisparos;
    [HideInInspector]
    public bool invencible = false;

    GameManager manager;
    Animator animator;
   
    protected float health;
    float xmin;
    float xmax;
    float ymin;
    float ymax;
    float margenEscenario = 0.4f;    
    float ButtonCooler = 0.2f;
    int ButtonCount = 0;
       
    bool sigueVivo = true;
    Text textoVidas;
    Slider barraVida;
    SpriteRenderer spriteRender;
    AudioSource audioSource;   
    GameObject botonItem;



    // EVENTOS ---------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        animator = GetComponent<Animator>();
        puedeDisparar = 1;
        botonItem = GameObject.Find("BotonItem");

    }

    void Start () {
        disparador = transform.Find("Disparador").gameObject;
        contenedorDisparos = GameObject.Find("ContenedorDisparos").gameObject;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();               
        health = saludInicial;
        textoVidas = GameObject.Find("TextoVidas").GetComponent<Text>();
        textoVidas.text = vidas.ToString();
        barraVida = GameObject.Find("BarraVida").GetComponent<Slider>();
        barraVida.maxValue = health;
        barraVida.value = health;

        float distanciaCamara = transform.position.z - Camera.main.transform.position.z;

        Vector3 extremoIzquierdo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanciaCamara));
        Vector3 extremoDerecho = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanciaCamara));
        xmin = extremoIzquierdo.x + margenEscenario;
        xmax = extremoDerecho.x - margenEscenario;

        Vector3 extremoArriba = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanciaCamara));
        Vector3 extremoAbajo = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distanciaCamara));
        ymin = extremoArriba.y + margenEscenario;
        ymax = extremoAbajo.y - margenEscenario;

        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
    }


    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Item item = collision.gameObject.GetComponentInChildren<Item>();
        if (!item)
            item = collision.gameObject.GetComponentInParent<Item>();
        if (item)
        {
            item.RecogerItem();
            audioSource.PlayOneShot(item.sonidoObtenerItem, item.sonidoObtenerItemVolumen);
            GuardarItem(item);
        }

        if (!invencible)
        {
            Disparo disparo = collision.gameObject.GetComponentInChildren<Disparo>();
            if(!disparo)
                disparo = collision.gameObject.GetComponentInParent<Disparo>();

            Enemigo enemigo = collision.gameObject.GetComponentInChildren<Enemigo>();
            if (!enemigo)
                enemigo = collision.gameObject.GetComponentInParent<Enemigo>();

            if (disparo)
            {
                if (disparo.bando == Bando.enemigo)
                {
                    RecibirDanio(disparo.damage);
                    disparo.Destruir();
                }
            }
            if (enemigo)
            {
                RecibirDanio(enemigo.damageChoque);
                enemigo.RecibeDanio(damageChoque);
            }      
        }

    }


    // Update is called once per frame
    void FixedUpdate ()
    {            
              
    }


    private void Update()
    {
        //posicion del jugador maxima 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xmin, xmax), Mathf.Clamp(transform.position.y, ymin, ymax));
        
    }


    // METODOS --------------------------------------------------------------------------------------------------------------------------




    private void GuardarItem(Item itemVal)
    {
       
        botonItem.GetComponent<Button>().interactable = true;
        Sprite spr = itemVal.sprite;  
        botonItem.transform.GetChild(0).GetComponent<Image>().sprite = spr;
        item = itemVal;

    }


    /// <summary>
    /// se manda llamar del UI del boton del item
    /// </summary>
    public void UsarItem()
    {
        if (botonItem.GetComponent<Button>().interactable)
        {
            switch (item.tipoItem)
            {
                case TipoItem.fuego:
                    Vector2 inicioDisparo = new Vector2(disparador.transform.position.x, disparador.transform.position.y);
                    Instantiate(item.objetoItemDisparo, inicioDisparo, disparador.transform.rotation, contenedorDisparos.transform);
                    tiempoUltimoDisparo = Time.time;
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


    private void Voltarse()
    {
        // dos comandos para voltear
        if (Input.GetButtonDown("Voltear"))
        {
            if (this.orientacion == OrientacionHorizontal.derecha)
            {
                transform.rotation = new Quaternion(0, 1, 0, 0);
                this.orientacion = OrientacionHorizontal.izquierda;
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                this.orientacion = OrientacionHorizontal.derecha;
            }
        }
        //doble tap media vuelta
        if (Input.GetButtonDown("Horizontal"))
        {
            float moverHorizontal = Input.GetAxis("Horizontal");

            if (ButtonCooler > 0 && ButtonCount == 1)
            {
                if (moverHorizontal < 0)
                {
                    transform.rotation = new Quaternion(0, 1, 0, 0);
                    this.orientacion = OrientacionHorizontal.izquierda;
                }
                if (moverHorizontal > 0)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    this.orientacion = OrientacionHorizontal.derecha;
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

       
    public void RecibirDanio(float danio)
    {
        health -= danio;
        if (health <= 0)
        {
            Muere();
        }
        else
        {
            barraVida.value = health;
            Instantiate(danioPrefab, transform, false);           
            //CameraShaker.Instance.ShakeOnce(4f, 4f, 0.05f, 0.5f);
            audioSource.PlayOneShot(sonidoDanio, 1);

            //StartCoroutine(AnimacionDanioBlink());
            animator.SetBool("DanioParpadeo", true);
        }
    }

    public void Muere()
    {
        if (sigueVivo)
        {
            sigueVivo = false;
            vidas--;
            textoVidas.text = vidas.ToString();       
            audioSource.PlayOneShot(sonidoMuerte, 1);
            //CameraShaker.Instance.ShakeOnce(4f, 4f, 0.05f, 0.5f);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            if (vidas == 0)
            {
                Destroy(this.gameObject);
                manager.GameOver();
            }
            else
            {
                health = saludInicial;
                barraVida.value = health;
                this.transform.position = posicionInicial;
                sigueVivo = true;
            }
        }
       
    }

    private void AsignarInvencible(int value)
    {
        invencible = Convert.ToBoolean(value);
        if (invencible)
        {
            animator.SetBool("GirarEsquivar", false);
            animator.SetBool("DanioParpadeo", false);
        }
           
    }


    public void GirarEsquivar() //tambien llamdo desde boton UI para mobil
    {
        if (!invencible)
            animator.SetBool("GirarEsquivar", true);
    }

    // Corrutinas -----------------------------------------------------------------------------------------------
   
         
    
    private IEnumerator AnimacionDanioBlink()
    {
            invencible = true;
            float tiempoParpadear = 5f * Time.deltaTime;
            float tiempoParpadeoTranscurrido = 0.0f;

            while (tiempoParpadeoTranscurrido < 40f * Time.deltaTime)
            {
                spriteRender.color = new Color(1, 1, 1, 0);
                tiempoParpadeoTranscurrido += tiempoParpadear;
                yield return new WaitForSeconds(tiempoParpadear);
                spriteRender.color = new Color(1, 1, 1, 1);
                tiempoParpadeoTranscurrido += tiempoParpadear * 1.2f;
                yield return new WaitForSeconds(tiempoParpadear * 1.2f);
            }

            spriteRender.color = new Color(1, 1, 1, 1);
            invencible = false;
            yield break;     
    }
}
