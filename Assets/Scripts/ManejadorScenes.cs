/* Para moverte entre scenes y darle un tiempo de espera antes de cargar 
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorScenes : MonoBehaviour
{
    public float tiempoCargaAutNivel = 0;

    private void Start()
    {
        if (tiempoCargaAutNivel > 0f)
        {
            Invoke("CargarSigEscena", tiempoCargaAutNivel);
        }
    }

    /// <summary>
    /// Carga la SCENE siguiente al actual en base al index
    /// </summary>
    public void CargarSigEscena()
    {
        string name = SceneManager.GetActiveScene().name;
        if (!string.IsNullOrEmpty(name))
        {
            int indiceSceneSiguiente = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(indiceSceneSiguiente); //anteriormente: Application.LoadLevel(nme);
            Debug.Log("Carga de la siguiente Scene: " + SceneManager.GetSceneAt(indiceSceneSiguiente).name);
        }
    }

    /// <summary>
    /// Carga una Scene en base a su nombre
    /// </summary>
    /// <param name="name">nombre de la Scene a cargar</param>
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
