using UnityEngine;

public enum Bando
{
    amigo,
    enemigo
}



public class Disparo : MonoBehaviour
{

    public float damage = 10f;
    public float tiempoDestruir;
    public float disparoVelocidad = 20f;
    public float tiempoEntreDisparos = 0.3f;  // Allow 3 shots per second   
    public Bando bando = Bando.enemigo;
    public AudioClip sonidoDisparo;
    public GameObject efectoImpacto;
    public bool TieneAnimacionDestruir = false;

    protected Jugador jugador;



    private void Awake()
    {
        jugador = GameObject.Find("Jugador").GetComponent<Jugador>();
    }

    protected void Start()
    {
        AudioSource audioSource = jugador.GetComponent<AudioSource>();
        if (sonidoDisparo)
        {
            audioSource.clip = sonidoDisparo;
            audioSource.Play();
        }
    }

    public void Destruir()
    {
        if (!TieneAnimacionDestruir)
        {
            Destroy(gameObject, tiempoDestruir);
            if (efectoImpacto)
                Instantiate(efectoImpacto, transform.position, transform.rotation, GameObject.Find("ContenedorDisparos").gameObject.transform);
        }
    }






}
