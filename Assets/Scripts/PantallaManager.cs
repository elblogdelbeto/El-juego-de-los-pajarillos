using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaManager : MonoBehaviour {

    public void LoadLevel(string name)
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
