using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NivelCampaniaManager : MonoBehaviour
{

    public Slider sliderProgresoNivel;
    public NivelEstructura nivelEstructura;

    static public int numNivel = 1;
    public Meta meta;
    GameManager gameManager;
    Fondo fondo;
    int enemigosDesplegados = 0;
    Dictionary<string, GameObject> DiccionarioEnemigos = new Dictionary<string, GameObject>();


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        fondo = FindObjectOfType<Fondo>();
    }

    // Use this for initialization
    void Start()
    {

        nivelEstructura = CargarNivelDesdeArchivo(numNivel);
        sliderProgresoNivel.maxValue = nivelEstructura.duracionTiempo;
        ArrancarNivel(nivelEstructura);

    }

    // Update is called once per frame
    void Update()
    {

        CorrerNivel(nivelEstructura);

        //actualizar slider en base al tiempo
        if (sliderProgresoNivel.value < sliderProgresoNivel.maxValue)
            sliderProgresoNivel.value = Time.timeSinceLevelLoad;

    }



    //METODOS-------------------------------------------------------------------------------------------------------------------

    public void TerminarNivel()
    {

    }

    private NivelEstructura CargarNivelDesdeArchivo(int nivel)
    {
        Debug.Log("Cargar Nivel: " + numNivel);

        NivelEstructura.EnemigoNivel level =
             new NivelEstructura.EnemigoNivel()
             {
                 identificadorEnemigo = "PajaroHuevo",
                 saludEnemigo = 100,
                 tiempoAparecer = 3f,
                 velocidad = 10f,
                 posicionAparecer = new Vector2(5, 0),
                 lugarAparecer = LugarAparecer.derecha,
                 dificultadEnemigo = DificultadEnemigo.normal,
                 inicioAleatorio = true,
                 reaparecer = false,
                 puntosEnemigo = 0
             };
        List<NivelEstructura.EnemigoNivel> enemigosNivel = new List<NivelEstructura.EnemigoNivel>();
        enemigosNivel.Add(level);
        NivelEstructura nivelEstructura = new NivelEstructura()
        {
            duracionTiempo = 60f,
            fondo = "fondo01",
            musica = "musica01",
            velocidadFondo = 5f,
            enemigosNivel = enemigosNivel,
            porcentajePuntosMediaEstrella = 50,
            porcentajePuntosEstrella = 100,
            porcentajePunteriaMediaEstrella = 50,
            porcentajePunteriaEstrella = 100,
            porcentajeSaludMediaEstrella = 50,
            porcentajeSaludEstrella = 100
        };
        //// serialize object to JSON
        string jsonString = JsonUtility.ToJson(nivelEstructura, true);
        //Debug.Log(jsonString);


        //USE TextAsset to load data
        TextAsset txtAsset = (TextAsset)Resources.Load("NivelCampania_" + numNivel, typeof(TextAsset));
        string tileFile = txtAsset.text;
        Debug.Log(tileFile);
        return JsonUtility.FromJson<NivelEstructura>(tileFile);
    }


    private void ArrancarNivel(NivelEstructura nivel)
    {
        fondo.velocidadScroll = nivel.velocidadFondo;
        //cargar fonso
        SpriteRenderer[] sprites = fondo.GetComponentsInChildren<SpriteRenderer>();
        Sprite fondoSprite = Resources.Load(nivel.fondo, typeof(Sprite)) as Sprite;
        foreach (SpriteRenderer item in sprites)
        {
            item.sprite = fondoSprite;

        }

        MusicPlayer.musicPlayer.ReproducirMusica(nivel.musica);

    }

    private void CorrerNivel(NivelEstructura nivel)
    {
        for (int i = enemigosDesplegados; i < nivel.enemigosNivel.Count; i++)
        {
            if ((i + gameManager.ContadorEnemigosPantalla - enemigosDesplegados) > gameManager.enemigosMaximosPantalla)
            {
                break;
            }
            NivelEstructura.EnemigoNivel enem = nivel.enemigosNivel[i];
            if ((Time.timeSinceLevelLoad) >= enem.tiempoAparecer)
            {

                if (!DiccionarioEnemigos.ContainsKey(enem.identificadorEnemigo))
                {
                    var go = Resources.Load<GameObject>(enem.identificadorEnemigo);
                    if (go == null)
                    {
                        Debug.Log("tried to instantate enemigo " + enem.identificadorEnemigo + ". but it does not exist");
                        return;
                    }
                    DiccionarioEnemigos.Add(enem.identificadorEnemigo, go);
                }
                GameObject goEnem = DiccionarioEnemigos[enem.identificadorEnemigo];
                goEnem.GetComponent<Enemigo>().inicioAleatorio = enem.inicioAleatorio;
                goEnem.GetComponent<Enemigo>().reaparecer = enem.reaparecer;

                Vector2 auxLugarAparecer = new Vector2();
                switch (enem.lugarAparecer)
                {
                    case LugarAparecer.arriba:
                        auxLugarAparecer = new Vector2(enem.posicionAparecer.x, gameManager.extremoArriba.y + enem.posicionAparecer.y);
                        break;
                    case LugarAparecer.derecha:
                        auxLugarAparecer = new Vector2(gameManager.extremoDerecha.x + enem.posicionAparecer.x, enem.posicionAparecer.y);
                        break;
                    case LugarAparecer.abajo:
                        auxLugarAparecer = new Vector2(enem.posicionAparecer.x, gameManager.extremoAbajo.y + enem.posicionAparecer.y);
                        break;
                    case LugarAparecer.izquierda:
                        auxLugarAparecer = new Vector2(gameManager.extremoIzquirda.x + enem.posicionAparecer.x, enem.posicionAparecer.y);
                        break;
                    default:
                        break;
                }

                if (nivel.enemigosNivel[i].puntosEnemigo <= 0)
                    nivel.enemigosNivel[i].puntosEnemigo = goEnem.GetComponent<Enemigo>().puntosQueDa;

                Instantiate(goEnem, auxLugarAparecer, Quaternion.identity, gameManager.contenedorEnemigos.transform);
                enemigosDesplegados++;
                gameManager.ContadorEnemigosPantalla++;
            }
        }
        meta.gameObject.SetActive(true);
        meta.transform.localPosition = new Vector2(10, 0);


    }




}
