using UnityEngine;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{

    public Slider sliderVolumen;

    public void AsignarVolumen()
    {
        GameObject manejadorMusica = GameObject.Find("ManejadorMusica");
        AudioSource audioSource = manejadorMusica.GetComponent<AudioSource>();
        audioSource.volume = sliderVolumen.value * 0.01f;
    }


}
