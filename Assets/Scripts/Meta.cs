using UnityEngine;
using UnityEngine.UI;

public class Meta : MonoBehaviour
{

    public GameObject panelFinal;
    public GameObject GUI;
    public GameObject contenedorEnemigos;
    public Jugador jugador;

    Fondo fondo;
    Vector3 posicionInicial;
    NivelCampaniaManager nivelCampaniaManager;


    private void Awake()
    {
        nivelCampaniaManager = FindObjectOfType<NivelCampaniaManager>();
        fondo = FindObjectOfType<Fondo>();
    }



    // Use this for initialization
    void Start()
    {

        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = posicionInicial + Vector3.left * Time.time * fondo.velocidadScroll * 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Jugador>())
        {
            nivelCampaniaManager.TerminarNivel();
            panelFinal.SetActive(true);
            MusicPlayer.musicPlayer.DetenerMusica();
            jugador.puedeDisparar = 0;

            Button[] botones = GUI.GetComponentsInChildren<Button>();
            foreach (Button boton in botones)
            {
                boton.interactable = false;
            }

            Enemigo[] enemigos = contenedorEnemigos.GetComponentsInChildren<Enemigo>();
            foreach (Enemigo enemigo in enemigos)
            {
                enemigo.MuereEnemigo(false);
            }


        }

    }







}
