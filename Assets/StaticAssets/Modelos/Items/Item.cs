using UnityEngine;

public enum TipoItem
{
    fuego,
    dinamita,
    iman,
    regalo,
    piedras
}

public class Item : MonoBehaviour
{

    public Fondo fondoImagen;
    public float velocidadHorizontal = 1;
    public float velocidadVertical = 1;
    public bool verticalInvertido = false;
    public GameObject efectoObtenerItem;
    public GameObject objetoItemDisparo;
    public AudioClip sonidoObtenerItem;
    public float sonidoObtenerItemVolumen = 0.5f;
    public TipoItem tipoItem;
    public Sprite sprite;

    Rigidbody2D rb;
    float velocidadFondo;

    // Use this for initialization
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        if (fondoImagen)
            velocidadFondo = fondoImagen.velocidadScroll / 6;
        else
            Debug.Log("falta asignar Fondo en editor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.gravityScale = rb.gravityScale * velocidadVertical;
        if (verticalInvertido)
            rb.gravityScale = rb.gravityScale * (-1);

        rb.AddForce(new Vector2(-(velocidadFondo * velocidadHorizontal), 0));
    }

    public void RecogerItem()
    {
        Destroy(gameObject, 0.05f);
        Instantiate(efectoObtenerItem, transform.position, transform.rotation);

    }

}
