using System.Collections;
using UnityEngine;

public enum DificultadEnemigo
{
    facil,
    normal
}


//[RequireComponent(typeof(Collider2D))]
public class Enemigo : MonoBehaviour
{

    public float health = 150f;
    public int puntosQueDa = 150;
    public float velocidad = 10f;
    public AudioClip sonidoMuerte;
    public DificultadEnemigo dificultadEnemigo;
    public float damageChoque = 20;
    public GameObject explosionPrefab;
    public int valorOcupaPantalla = 1;
    public float tiempoDestruir = 0.05f;
    public bool inicioAleatorio = true;
    public bool reaparecer = false;
    public LugarAparecer lugarAparecerAleatorio = LugarAparecer.derecha;

    [HideInInspector]
    public OrientacionHorizontal orientacionHorizontal = OrientacionHorizontal.izquierda;

    [SerializeField]
    protected Material materialBlanquear;

    protected ScoreKeeper scoreKeeper;
    protected GameManager gameManager;
    protected HordaManager hordaManager;   
    protected Material materialOriginal;    
    protected bool sigueVivo = true;


    // EVENTOS ----------------------------------------------------------------------------------------------------------

    protected void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hordaManager = FindObjectOfType<HordaManager>();
        materialOriginal = gameObject.GetComponentInChildren<SpriteRenderer>().material;
    }


    protected void Start()
    {
        
    }

    
    /// <summary>
    ///  Si colisiona con el jugador o disparo de jugador   
    /// </summary>
    /// <param name="collision">colision de jugador o disparo</param>   
    protected void OnTriggerStay2D(Collider2D collision)
    {
        Disparo disparo = collision.gameObject.GetComponent<Disparo>();
        if (!disparo)
        {
            disparo = collision.gameObject.GetComponentInParent<Disparo>();
        }
        if (disparo)
        {
            if (disparo.bando == Bando.amigo)
            {
                RecibeDanio(disparo.damage);
                disparo.Destruir();
                gameManager.contenedorDisparos.disparosAcertados++;
            }
        }
    }




    // METODOS ----------------------------------------------------------------------------------------------------------
   
    public void RecibeDanio(float danio)
    {
        health -= danio;
        if (health <= 0)
        {
            MuereEnemigo(false);
        }
        else
        {
            StartCoroutine(AnimacionDanio());
        }
    }



    public void MuereEnemigo(bool eliminadoPorDestructor = false)
    {
        if (sigueVivo)
        {
            sigueVivo = false;

            gameManager.ContadorEnemigosPantalla--;

            if (hordaManager)
            {
                hordaManager.contadorEnemigosPantalla--;
                hordaManager.EnemigosRestantesOrda--;
                hordaManager.textoEnemigos.text = hordaManager.EnemigosRestantesOrda.ToString();
            }

            if (!eliminadoPorDestructor)
            {
                Collider2D cuerpoEnemigo = gameObject.GetComponentInChildren<Collider2D>();
                Instantiate(explosionPrefab, cuerpoEnemigo.transform.position, cuerpoEnemigo.transform.rotation);
                if (sonidoMuerte)
                    AudioSource.PlayClipAtPoint(sonidoMuerte, Camera.main.transform.position);
                scoreKeeper.Score(puntosQueDa);
            }
            Destroy(gameObject, tiempoDestruir);

        }

    }



    // CORRUTINAS ----------------------------------------------------------------------------------------------------------


    private IEnumerator AnimacionDanio()
    {
        SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer item in sprites)
        {
            item.material = materialBlanquear;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (SpriteRenderer item in sprites)
        {
            item.material = materialOriginal;
        }
    }



}
