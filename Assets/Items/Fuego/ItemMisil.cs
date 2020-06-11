using UnityEngine;

public class ItemMisil : MonoBehaviour
{

    private Disparo disparo;
    private Jugador jugador;


    private void Awake()
    {
        disparo = gameObject.GetComponent<Disparo>();
        jugador = GameObject.FindObjectOfType<Jugador>();

    }


    private void Start()
    {
        Desplegar();
    }


    protected void Desplegar()
    {
        OrientacionHorizontal orientacion = jugador.orientacion;

        float Y = jugador.disparador.transform.rotation.eulerAngles.z;
        if (Y > 180)
            Y = Y - 360;
        if (orientacion == OrientacionHorizontal.derecha)
            GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(disparo.disparoVelocidad, Y / 5);
        else
            GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(disparo.disparoVelocidad * -1, Y / 5);
    }



    public void DestruirObjetoMisil()
    {
        Destroy(gameObject, 0.05f);
    }


}



