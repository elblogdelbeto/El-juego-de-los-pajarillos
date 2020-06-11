using UnityEngine;
using UnityEngine.UI;

public class PantallaFinNivel : MonoBehaviour
{


    public Text textoCantidadPuntos;
    public Sprite mediaEstrella;
    public Sprite estrellaCompleta;
    public NivelCampaniaManager nivelCampaniaManager;
    public Image estrellaPuntos;


    private void Awake()
    {

    }




    public void MostrarPuntos() //llamado desde animtor
    {
        int puntosObtenidos = ScoreKeeper.score;
        textoCantidadPuntos.text = puntosObtenidos.ToString();
        int puntosTotalesPosibles = 0;
        foreach (NivelEstructura.EnemigoNivel enem in nivelCampaniaManager.nivelEstructura.enemigosNivel)
        {
            puntosTotalesPosibles += enem.puntosEnemigo;
        }
        float porcentajeLogrado = ((float)puntosObtenidos / puntosTotalesPosibles) * 100f;
        if (porcentajeLogrado >= nivelCampaniaManager.nivelEstructura.porcentajePuntosEstrella)
        {
            estrellaPuntos.sprite = estrellaCompleta;
        }
        else if (porcentajeLogrado >= nivelCampaniaManager.nivelEstructura.porcentajePuntosMediaEstrella)
        {
            estrellaPuntos.sprite = mediaEstrella;
        }


    }

}
