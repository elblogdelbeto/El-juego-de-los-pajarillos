//Encargado de manejar la musica durante todo el juego se crea en cada escena si no está creado ya (simulado singleton)

using UnityEngine;

using UnityEngine.SceneManagement;


public class MusicPlayer : MonoBehaviour
{

    public static MusicPlayer musicPlayer;

    public AudioClip[] musicas;
    private AudioSource audioSource;

    void Awake()
    {

        if (musicPlayer == null)
        {
            DontDestroyOnLoad(gameObject);
            musicPlayer = this;

        }
        else if (musicPlayer != this)
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioSource.clip = musicas[SceneManager.GetActiveScene().buildIndex];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ReproducirMusica(string nombreMusica)
    {
        audioSource.clip = Resources.Load(nombreMusica, typeof(AudioClip)) as AudioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void DetenerMusica()
    {
        audioSource.Stop();
    }


}
