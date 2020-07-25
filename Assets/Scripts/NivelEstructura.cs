using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NivelEstructura
{


    [Serializable]
    public class EnemigoNivel
    {
        public string identificadorEnemigo;
        public float tiempoAparecer;
        public bool inicioAleatorio = true;
        public Vector2 posicionAparecer;
        public LugarAparecer lugarAparecer = LugarAparecer.derecha;
        public LugarAparecer lugarAparecerAleatorio = LugarAparecer.derecha;
        public bool reaparecer = false;
        public DificultadEnemigo dificultadEnemigo;
        public float velocidad;
        public float saludEnemigo;
        public int puntosEnemigo;
    }

    public string fondo;
    public float velocidadFondo;
    public float duracionTiempo;
    public float porcentajePuntosMediaEstrella;
    public float porcentajePuntosEstrella;
    public float porcentajePunteriaMediaEstrella;
    public float porcentajePunteriaEstrella;
    public float porcentajeSaludMediaEstrella;
    public float porcentajeSaludEstrella;
    public string musica;
    public List<EnemigoNivel> enemigosNivel;


}


public enum LugarAparecer
{
    arriba, //0
    derecha,  //1
    abajo,  //2
    izquierda,  //3
    vertical,  //4 
    horizontal,  //5
    frente,  //6
    todo   //7
}




