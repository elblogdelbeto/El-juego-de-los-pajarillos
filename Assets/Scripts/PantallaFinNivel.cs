using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PantallaFinNivel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textoCantidadPuntos;
    [SerializeField] TextMeshProUGUI textoPorcentajePunteria;
    [SerializeField] TextMeshProUGUI textoSaludPerdida;
    [SerializeField] Sprite mediaEstrella;
    [SerializeField] Sprite estrellaCompleta;
    [SerializeField] NivelCampaniaManager nivelCampaniaManager;
    [SerializeField] Image estrellaPuntos;
    [SerializeField] Image estrellaPunteria;
    [SerializeField] Image estrellaSalud;
    [SerializeField] Image estrellaTotales1;
    [SerializeField] Image estrellaTotales2;
    [SerializeField] Image estrellaTotales3;

    ManagerDisparos managerDiaparos;
    float estrellasTotalesObtenidas = 0f;

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
            estrellasTotalesObtenidas += 1;
        }
        else if (porcentajeLogrado >= nivelCampaniaManager.nivelEstructura.porcentajePuntosMediaEstrella)
        {
            estrellaPuntos.sprite = mediaEstrella;
            estrellasTotalesObtenidas += 0.5f;
        }
    }


    public void MostrarPunteria() //llamado desde animator
    {
        float disparosAcertadosPorcentaje = managerDiaparos.disparosAcertados / managerDiaparos.disparosDesplegados * 100;
        textoPorcentajePunteria.text = Mathf.Round(disparosAcertadosPorcentaje).ToString() + "%";

        if (disparosAcertadosPorcentaje >= nivelCampaniaManager.nivelEstructura.porcentajePunteriaEstrella)
        {
            estrellaPunteria.sprite = estrellaCompleta;
            estrellasTotalesObtenidas += 1;
        }
        else if (disparosAcertadosPorcentaje >= nivelCampaniaManager.nivelEstructura.porcentajePunteriaMediaEstrella)
        {
            estrellaPunteria.sprite = mediaEstrella;
            estrellasTotalesObtenidas += 0.5f;
        }
    }

    public void MostrarSaludEstrellas() //llamado desde animator
    {
        int vidasPerdidas = nivelCampaniaManager.jugador.vidas - nivelCampaniaManager.jugador.vidasIniciales;
        float saludPerdida = nivelCampaniaManager.jugador.salud - nivelCampaniaManager.jugador.saludInicial;
        saludPerdida += vidasPerdidas * nivelCampaniaManager.jugador.saludInicial;

        textoSaludPerdida.text = Mathf.Round(saludPerdida).ToString();

        if (saludPerdida >= nivelCampaniaManager.nivelEstructura.porcentajeSaludEstrella)
        {
            estrellaSalud.sprite = estrellaCompleta;
            estrellasTotalesObtenidas += 1;
        }
        else if (saludPerdida >= nivelCampaniaManager.nivelEstructura.porcentajeSaludMediaEstrella)
        {
            estrellaSalud.sprite = mediaEstrella;
            estrellasTotalesObtenidas += 0.5f;
        }
    }

    public void MostrarEstrellasTotales() //llamado desde animator
    {
        switch (estrellasTotalesObtenidas)
        {
            case 0.5f:
                estrellaTotales1.sprite = mediaEstrella;
                break;
            case 1f:
                estrellaTotales1.sprite = estrellaCompleta;
                break;
            case 1.5f:
                estrellaTotales1.sprite = estrellaCompleta;
                estrellaTotales2.sprite = mediaEstrella;
                break;
            case 2f:
                estrellaTotales1.sprite = estrellaCompleta;
                estrellaTotales2.sprite = estrellaCompleta;
                break;
            case 2.5f:
                estrellaTotales1.sprite = estrellaCompleta;
                estrellaTotales2.sprite = estrellaCompleta;
                estrellaTotales3.sprite = mediaEstrella;
                break;
            case 3f:
                estrellaTotales1.sprite = estrellaCompleta;
                estrellaTotales2.sprite = estrellaCompleta;
                estrellaTotales3.sprite = estrellaCompleta;
                break;

            default:
                break;
        }
    }

}
