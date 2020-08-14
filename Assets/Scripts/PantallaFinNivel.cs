using UnityEngine;
using UnityEngine.UI;

public class PantallaFinNivel : MonoBehaviour
{

    [SerializeField] Text textoCantidadPuntos;
    [SerializeField] Text textoPorcentajePunteria;
    [SerializeField] Sprite mediaEstrella;
    [SerializeField] Sprite estrellaCompleta;
    [SerializeField] NivelCampaniaManager nivelCampaniaManager;
    [SerializeField] Image estrellaPuntos;
    [SerializeField] Image estrellaPunteria;
    [SerializeField] Image estrellaSalud;

    ManagerDisparos managerDiaparos;

    private void Awake()
    {
        managerDiaparos = FindObjectOfType<ManagerDisparos>();
    }

    public void MostrarPuntos() //llamado desde animator
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


    public void MostrarPunteria() //llamado desde animator
    {
        float disparosAcertadosPorcentaje = managerDiaparos.disparosAcertados / managerDiaparos.disparosDesplegados * 100;
        textoPorcentajePunteria.text = Mathf.Round(disparosAcertadosPorcentaje).ToString() + "%";

        if (disparosAcertadosPorcentaje >= nivelCampaniaManager.nivelEstructura.porcentajePunteriaEstrella)
        {
            estrellaPunteria.sprite = estrellaCompleta;
        }
        else if (disparosAcertadosPorcentaje >= nivelCampaniaManager.nivelEstructura.porcentajePunteriaMediaEstrella)
        {
            estrellaPunteria.sprite = mediaEstrella;
        }
    }

    public void MostrarSaludEstrellas() //llamado desde animator
    {
        int vidasPerdidas = nivelCampaniaManager.jugador.vidasIniciales - nivelCampaniaManager.jugador.vidas;
        float disparosAcertadosPorcentaje = managerDiaparos.disparosAcertados / managerDiaparos.disparosDesplegados * 100;
        textoPorcentajePunteria.text = Mathf.Round(disparosAcertadosPorcentaje).ToString() + "%";

        if (disparosAcertadosPorcentaje >= nivelCampaniaManager.nivelEstructura.porcentajePunteriaEstrella)
        {
            estrellaPunteria.sprite = estrellaCompleta;
        }
        else if (disparosAcertadosPorcentaje >= nivelCampaniaManager.nivelEstructura.porcentajePunteriaMediaEstrella)
        {
            estrellaPunteria.sprite = mediaEstrella;
        }
    }

}
