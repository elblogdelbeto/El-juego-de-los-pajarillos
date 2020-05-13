using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRojo : Disparo {
 
    public LaserRojo()
    {
        this.damage = 100f;
        this.bando = Bando.amigo;
    }

    new private void Start()
    {
        base.Start();
        Desplegar();
    }


    protected void Desplegar()
    {        
        OrientacionHorizontal orientacion = jugador.orientacion;
        
        float Y = jugador.disparador.transform.rotation.eulerAngles.z;
        if (Y > 180)
            Y = Y - 360;
        if (orientacion == OrientacionHorizontal.derecha)
                GetComponent<Rigidbody2D>().velocity = new Vector2(disparoVelocidad, Y/5);
        else
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(disparoVelocidad * -1, Y/5);       
    }


}
