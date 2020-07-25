using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public enum Dificultad
{
    superfacil,
    facil,
    normal,
    dificil
}

[Serializable]
public class ProgresoJuego
{
    public List<Nivel> niveles;
    public int estrellasCampania;
    public int monedasGanadas;
    public int monedasActuales;

}

[Serializable]
public class Nivel
{
    public int idNivel;
    public bool desbloqueado;
    public int estrellasSuperFacil;
    public int estrellasFacil;
    public int estrellasNormal;
    public int estrellasDificil;
}


public class GameManager : MonoBehaviour
{
    public float velocidadJuego = 1.0f;
    public int enemigosMaximosPantalla = 6;
    public bool gameOver = false;
    public static Dificultad dificultad;
    public int frames = 60;
    [HideInInspector]
    public static int monedasGanadas = 100;
    [HideInInspector]
    public static int monedasActuales = 10;
    [HideInInspector]
    public static int estrellasCampania = 5;
    [HideInInspector]
    public static List<Nivel> progresoNiveles;
    [HideInInspector]
    public Vector3 extremoAbajo;
    [HideInInspector]
    public Vector3 extremoArriba;
    [HideInInspector]
    public Vector3 extremoIzquierda;
    [HideInInspector]
    public Vector3 extremoDerecha;
    public int ContadorEnemigosPantalla { get; set; }
    public GameObject contenedorEnemigos;
    public GameObject contenedorDisparos;

    public static ManejadorScenes manejadorScenes;
    public static GameManager gameManager;


    // Awake is called when the script instance is being loaded
    private void Awake()
    {

        if (gameManager == null)
        {
            DontDestroyOnLoad(gameObject);

            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = frames;
            Time.timeScale = velocidadJuego;            
            manejadorScenes = GameObject.FindObjectOfType<ManejadorScenes>();

            float distanciaCamara = transform.position.z - Camera.main.transform.position.z;
                        
            extremoIzquierda = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanciaCamara));
            extremoDerecha = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanciaCamara));
            extremoAbajo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanciaCamara));
            extremoArriba = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distanciaCamara));


            gameManager = this;

        }
        else if (gameManager != this)
        {  
            Destroy(gameObject);
        }



    }



    //  ---- METODOS--------------------------------------------------------------------------------------------

    public void GameOver()
    {
        gameOver = true;
        GameManager.manejadorScenes.CargarEscena("03_GameOver_01");
    }

    public static void SalvarPartida()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SalvadaJuego.dat");

        ProgresoJuego data = new ProgresoJuego();
        data.estrellasCampania = 2;
        data.monedasActuales = monedasActuales;
        data.monedasGanadas = monedasGanadas;
        data.niveles = new List<Nivel>();
        Nivel niveluno = new Nivel();
        niveluno.idNivel = 1;
        niveluno.desbloqueado = true;
        niveluno.estrellasNormal = 2;
        data.niveles.Add(niveluno);
        Nivel nivel2 = new Nivel();
        nivel2.idNivel = 2;
        nivel2.desbloqueado = true;
        nivel2.estrellasNormal = 3;
        data.niveles.Add(nivel2);

        bf.Serialize(file, data);
        file.Close();

    }

    public static void CargarPartida()
    {
        if (File.Exists(Application.persistentDataPath + "/SalvadaJuego.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SalvadaJuego.dat", FileMode.Open);
            ProgresoJuego data = (ProgresoJuego)bf.Deserialize(file);
            file.Close();

            print("partida cargada: " + data);
            estrellasCampania = data.estrellasCampania;
            monedasGanadas = data.monedasGanadas;
            monedasActuales = data.monedasActuales;
            progresoNiveles = data.niveles;

        }

    }


}




