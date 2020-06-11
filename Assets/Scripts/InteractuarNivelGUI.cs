using UnityEngine;

public class InteractuarNivelGUI : MonoBehaviour
{

    public int idNivel;
    public GameObject confirmarNivel;

    private void OnMouseUpAsButton()
    {
        confirmarNivel.SetActive(true);
        NivelCampaniaManager.numNivel = idNivel;
    }


    public void AbrirNivelConfirmacion()
    {
        confirmarNivel.SetActive(true);
        NivelCampaniaManager.numNivel = idNivel;
    }





}
