/* Para moverte entre scenes y darle un tiempo de espera antes de cargar 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorScenes : MonoBehaviour {

    public float tiempoCargaAutNivel = 0;


    private void Start()
    {
        if (tiempoCargaAutNivel > 0f)
            Invoke("CargarSigEscena", tiempoCargaAutNivel);
    }

    public void CargarSigEscena()
    {
        string name = SceneManager.GetActiveScene().name;
        if (!string.IsNullOrEmpty(name))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //anteriormente: Application.LoadLevel(name);
            Debug.Log("Carga del nuevo nivel: " + name);
        }
    }

    public void CargarEscena(string name)
    {
        Debug.Log("Carga del nuevo nivel: " + name);
        SceneManager.LoadScene(name); //anteriormente: Application.LoadLevel(name);
    }

    public void QuitarJuego()
    {
        Debug.Log("Solicitud de salir");
        Application.Quit();

    }




}
