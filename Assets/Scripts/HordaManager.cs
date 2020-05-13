using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HordaManager : MonoBehaviour {

    public GameObject[] enemigos;
    public float esperaOrda = 6f;
    public float esperaOleada = 3f;
    public float esperaEnemigo = 0.5f;
    public int totalEnemigosOrda = 10;
    public int cantidadEnemigosOleada = 3;
    public int EnemigosRestantesOrda
    {
        get
        {
            return enemigosRestantesOrda;
        }
        set
        {
            enemigosRestantesOrda = value;
        }
    }
    [HideInInspector]
    public Text textoEnemigos;
    [HideInInspector]
    public Text textoNumOrda;

    private GameManager gameManager;      
    private int noOrda = 1;
    private int enemigosRestantesOrda = 0;
    [HideInInspector]
    public int contadorEnemigosPantalla = 0;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.gameOver = false;
        EnemigosRestantesOrda = totalEnemigosOrda;
        textoEnemigos = GameObject.Find("TextoEnemigosRestantes").GetComponent<Text>();
        textoEnemigos.text = EnemigosRestantesOrda.ToString();
        textoNumOrda = GameObject.Find("NumOrda").GetComponent<Text>();
        textoNumOrda.text = noOrda.ToString();
        
    }



    // Use this for initialization
    void Start () {
         StartCoroutine(ConvocarOrda());
    }

    // Update is called once per frame
    void Update () {
		
	}


    //Metodos-----------------------------------------------------------------------------------

    private IEnumerator ConvocarOrda()
    {
        yield return new WaitForSeconds(esperaOrda);
        while (gameManager.gameOver == false)
        {
            if (EnemigosRestantesOrda > 0)
            {
                int numeroRepeticionesenemigo = Random.Range(1, cantidadEnemigosOleada);
                int indiceEnemigo = Random.Range(0, enemigos.Length);

                for (int i = 0; i < cantidadEnemigosOleada &&
                   contadorEnemigosPantalla < gameManager.enemigosMaximosPantalla &&
                   contadorEnemigosPantalla < EnemigosRestantesOrda; i++)
                {
                    if (numeroRepeticionesenemigo <= 0)
                    {
                        indiceEnemigo = Random.Range(0, enemigos.Length);
                        numeroRepeticionesenemigo = Random.Range(1, cantidadEnemigosOleada);
                    }
                    else
                    {
                        numeroRepeticionesenemigo--;
                    }
                    GameObject goEnemigo = enemigos[indiceEnemigo];
                    contadorEnemigosPantalla++;                   
                    Instantiate(goEnemigo, gameManager.contenedorEnemigos.transform, true);
                    yield return new WaitForSeconds(esperaEnemigo);
                }

                yield return new WaitForSeconds(esperaOleada); //yield return new WaitForEndOfFrame();
            }
            else
            {
                yield return new WaitForSeconds(esperaOrda);
                noOrda++;
                textoNumOrda.text = noOrda.ToString();
                enemigosRestantesOrda = totalEnemigosOrda; //TODO : + (noOrda * GameManager.dificultad);
                textoEnemigos.text = EnemigosRestantesOrda.ToString();
            }
        }
        yield break;
    }


}
