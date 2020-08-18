using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NivelCampaniaManager : MonoBehaviour
{

    public Slider sliderProgresoNivel;
    public NivelEstructura nivelEstructura;
    public Meta meta;
    public Jugador jugador;

    [SerializeField]
    static public int numNivel = 0;

    GameManager gameManager;
    Fondo fondo;
    Dictionary<string, GameObject> DiccionarioEnemigos = new Dictionary<string, GameObject>();
    int enemigosDesplegados = 0;   
    bool nivelTerminado = false;


    private void Awake()
    {        
        gameManager = FindObjectOfType<GameManager>(); 
        fondo = FindObjectOfType<Fondo>();
        jugador = FindObjectOfType<Jugador>();
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
        if (!nivelTerminado)
            CorrerNivel(nivelEstructura);
        else //nivel ya terminado
            jugador.puedeControlarse = false;


        //actualizar slider en base al tiempo
        if (sliderProgresoNivel.value < sliderProgresoNivel.maxValue)
            sliderProgresoNivel.value = Time.timeSinceLevelLoad;

        

    }



    //METODOS-------------------------------------------------------------------------------------------------------------------

    public void TerminarNivel()
    {
        nivelTerminado = true;
    }

    private NivelEstructura CargarNivelDesdeArchivo(int nivel)
    {
        Debug.Log("Cargar Nivel: NivelCampania_" + nivel);

        #region Ejemplo en codigo de la estructura de un nivel      
            //NivelEstructura.EnemigoNivel level =
            //     new NivelEstructura.EnemigoNivel()
            //     {
            //         identificadorEnemigo = "PajaroHuevo",
            //         saludEnemigo = 100,
            //         tiempoAparecer = 3f,
            //         velocidad = 10f,
            //         posicionAparecer = new Vector2(5, 0),
            //         lugarAparecer = LugarAparecer.derecha,
            //         dificultadEnemigo = DificultadEnemigo.normal,
            //         inicioAleatorio = true,
            //         reaparecer = false,
            //         puntosEnemigo = 0
            //     };
            //List<NivelEstructura.EnemigoNivel> enemigosNivel = new List<NivelEstructura.EnemigoNivel>();
            //enemigosNivel.Add(level);
            //NivelEstructura nivelEstructura = new NivelEstructura()
            //{
            //    duracionTiempo = 60f,
            //    fondo = "fondo01",
            //    musica = "musica01",
            //    velocidadFondo = 5f,
            //    enemigosNivel = enemigosNivel,
            //    porcentajePuntosMediaEstrella = 50,
            //    porcentajePuntosEstrella = 100,
            //    porcentajePunteriaMediaEstrella = 50,
            //    porcentajePunteriaEstrella = 100,
            //    porcentajeSaludMediaEstrella = 50,
            //    porcentajeSaludEstrella = 100
            //};
            ////// serialize object to JSON
            //string jsonString = JsonUtility.ToJson(nivelEstructura, true);
            ////Debug.Log(jsonString);
        #endregion

        //USE TextAsset to load data
        TextAsset txtAsset = (TextAsset)Resources.Load("NivelCampania_" + nivel, typeof(TextAsset));
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
        if (Time.timeSinceLevelLoad < nivel.duracionTiempo || enemigosDesplegados < nivel.enemigosNivel.Count)
        {
            if (enemigosDesplegados < nivel.enemigosNivel.Count)
            {
                NivelEstructura.EnemigoNivel enem = nivel.enemigosNivel[enemigosDesplegados];
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
                    Vector2 auxLugarAparecer = new Vector2();
                    switch (enem.lugarAparecer)
                    {
                        case LugarAparecer.arriba:
                            auxLugarAparecer = new Vector2((gameManager.extremoDerecha.x * enem.posicionAparecer.x), (gameManager.extremoArriba.y+1) + enem.posicionAparecer.y);
                            break;
                        case LugarAparecer.derecha:
                            auxLugarAparecer = new Vector2((gameManager.extremoDerecha.x+1) + enem.posicionAparecer.x, (gameManager.extremoDerecha.y * enem.posicionAparecer.y));
                            break;
                        case LugarAparecer.abajo:
                            auxLugarAparecer = new Vector2((gameManager.extremoDerecha.x * enem.posicionAparecer.x), (gameManager.extremoAbajo.y-1) + enem.posicionAparecer.y);
                            break;
                        case LugarAparecer.izquierda:
                            auxLugarAparecer = new Vector2((gameManager.extremoIzquierda.x-1) + enem.posicionAparecer.x, (gameManager.extremoDerecha.y * enem.posicionAparecer.y));
                            break;
                        default:
                            break;
                    }

                    Enemigo enemigoObjetoCargado = DiccionarioEnemigos[enem.identificadorEnemigo].GetComponent<Enemigo>();
                    if(!enemigoObjetoCargado)
                        enemigoObjetoCargado = DiccionarioEnemigos[enem.identificadorEnemigo].GetComponentInChildren<Enemigo>();
                    enemigoObjetoCargado.inicioAleatorio = enem.inicioAleatorio;
                    enemigoObjetoCargado.reaparecer = enem.reaparecer;
                    enemigoObjetoCargado.lugarAparecerAleatorio = enem.lugarAparecerAleatorio;                    
                    enemigoObjetoCargado.velocidad = enem.velocidad > 0 ? enem.velocidad : enemigoObjetoCargado.velocidad;
                    enemigoObjetoCargado.health = enem.saludEnemigo > 0 ? enem.saludEnemigo : enemigoObjetoCargado.health;                    
                    enemigoObjetoCargado.dificultadEnemigo = enem.dificultadEnemigo;
                    if (enem.puntosEnemigo <= 0)
                        enem.puntosEnemigo = enemigoObjetoCargado.puntosQueDa;
                    else
                        enemigoObjetoCargado.puntosQueDa = enem.puntosEnemigo;
                    

                    Instantiate(enemigoObjetoCargado, auxLugarAparecer, Quaternion.identity, gameManager.contenedorEnemigos.transform);
                    gameManager.ContadorEnemigosPantalla++;
                    enemigosDesplegados++;
                }
            }
        }
        if (Time.timeSinceLevelLoad > nivel.duracionTiempo && !meta.gameObject.activeSelf)
        {
            meta.transform.position = new Vector3(gameManager.extremoDerecha.x + 3, 0, 0);
            meta.gameObject.SetActive(true);
        }
        
        //}
        //meta.gameObject.SetActive(true);
        //meta.transform.localPosition = new Vector2(10, 0);


    }




}
