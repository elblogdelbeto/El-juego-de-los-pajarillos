using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum OrientacionHorizontal
{
    izquierda,
    derecha
}

[RequireComponent(typeof(TouchAndControls))]
public class Jugador : MonoBehaviour
{
    public bool puedeDisparar { get; set; } = true;
    public bool disparoAutomatico { get; set; } = false;
    public Animator animator { get; set; }

    public Vector2 posicionInicial = new Vector2(-6, 0);
    public GameObject disparador;
    public GameObject disparoPrefab;
    public GameObject explosionPrefab;
    public GameObject danioPrefab;   
    public float saludInicial = 100f;
    public float damageChoque = 50f;    
    public AudioClip sonidoMuerte;
    public AudioClip sonidoDanio;
    public Item item;

    [SerializeField] int vidas = 5;

    [HideInInspector] public OrientacionHorizontal orientacion = OrientacionHorizontal.derecha;
    [HideInInspector] public float tiempoUltimoDisparo = 0;
    [HideInInspector] public float inclinacionMovimiento = 15f;    
    [HideInInspector] public bool invencible = false;

    [SerializeField] GameManager gameManager;

    protected float salud;

    float xmin, xmax, ymin, ymax;

    float margenEscenario = 0.5f;
    float ButtonCooler = 0.2f;
    int ButtonCount = 0;
    bool sigueVivo = true;   
    Slider barraVida;
    SpriteRenderer spriteRender;
    AudioSource audioSource;
    TouchAndControls touchAndControls;
 


    // EVENTOS ---------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        animator = GetComponent<Animator>();        
        disparador = transform.Find("Disparador").gameObject;
        barraVida = GameObject.Find("BarraVida").GetComponent<Slider>();
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        touchAndControls = FindObjectOfType<TouchAndControls>();       

    }

    void Start()
    {
        if (!gameManager)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        salud = saludInicial;        
        barraVida.maxValue = salud;
        barraVida.value = salud;
        puedeDisparar = true;

        float distanciaCamara = transform.position.z - Camera.main.transform.position.z;

        xmin = gameManager.extremoIzquirda.x + margenEscenario;
        xmax = gameManager.extremoDerecha.x - margenEscenario;
        ymin = gameManager.extremoAbajo.y + margenEscenario;
        ymax = gameManager.extremoArriba.y - margenEscenario;
       
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
            if (!disparo)
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
    void FixedUpdate()
    {

    }


    private void Update()
    {
        //posicion del jugador maxima 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xmin, xmax), Mathf.Clamp(transform.position.y, ymin, ymax));

    }


    //Eventos llamados desde animaciones -------------------------------------------------------------------------------------------
    private void AsignarInvencible(int value)
    {
        invencible = Convert.ToBoolean(value);
        if (invencible)
        {
            animator.SetBool("GirarEsquivar", false);
            animator.SetBool("DanioParpadeo", false);
        }
    }

    private void AsignarPuedeDisparar(int value)
    {
        puedeDisparar = Convert.ToBoolean(value);
    }



    // METODOS --------------------------------------------------------------------------------------------------------------------------


    public int ConsultarVidas()
    {
        return vidas;
    }

    public bool ConsultarSiEstaVivo()
    {
        return sigueVivo;
    }

    private void GuardarItem(Item itemVal)
    {
        touchAndControls.botonItem.GetComponent<Button>().interactable = true;
        Sprite spr = itemVal.sprite;
        touchAndControls.botonItem.transform.GetChild(0).GetComponent<Image>().sprite = spr;
        item = itemVal;

    }


    public void Voltarse()
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
        salud -= danio;
        if (salud <= 0)
        {
            Muere();
        }
        else
        {
            barraVida.value = salud;
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
            audioSource.PlayOneShot(sonidoMuerte, 1);
            //CameraShaker.Instance.ShakeOnce(4f, 4f, 0.05f, 0.5f);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            if (vidas == 0)
            {
                Destroy(this.gameObject);
                gameManager.GameOver();
            }
            else
            {
                salud = saludInicial;
                barraVida.value = salud;
                this.transform.position = posicionInicial;
                sigueVivo = true;
            }
        }

    }




    // Corrutinas --------------------------------------------------------------------------------------------------------------------


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
