using UnityEngine;

public class MenuPausa : MonoBehaviour
{

    public static bool JuegoPausado = false;
    public ManejadorScenes manejadorScenes;
    public GameObject menuPausa;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("StartPausa"))
        {
            if (JuegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        JuegoPausado = true;

    }

    public void ReanudarJuego()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        JuegoPausado = false;
    }

    public void IrMenu()
    {
        Time.timeScale = 1f;
        JuegoPausado = false;
        manejadorScenes.CargarEscena("01_StartMenu_01");
    }
}
