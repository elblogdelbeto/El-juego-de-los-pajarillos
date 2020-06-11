using UnityEngine;

public class ReproducirSonidos : MonoBehaviour
{

    public AudioClip sonidoLevelComplete;
    public AudioClip sonidoAparecerLetrero;

    GameManager gameManager;
    AudioSource audioSource;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = gameManager.GetComponent<AudioSource>();

    }

    public void ReproducorSonido(int value) //llamado desde animation
    {
        switch (value)
        {
            case 1:
                audioSource.PlayOneShot(sonidoLevelComplete);
                break;
            case 2:
                audioSource.PlayOneShot(sonidoAparecerLetrero);
                break;
            default:
                audioSource.PlayOneShot(sonidoAparecerLetrero);
                break;
        }
    }
}
